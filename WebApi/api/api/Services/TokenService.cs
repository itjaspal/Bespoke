using api.DataAccess;
using api.Interfaces;
using api.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Transactions;
using System.Web;

namespace api.Services
{
    public class TokenService : ITokenService
    {
        public Token GenerateToken(String userId)
        {
            string token = Guid.NewGuid().ToString();
            DateTime issuedOn = DateTime.Now;
            DateTime expiredOn = DateTime.Now.AddMinutes(Convert.ToDouble(ConfigurationManager.AppSettings["AuthTokenExpiry"]));
            var tokendomain = new Token
            {
                UserId = userId,
                UserName = "",
                AuthToken = token,
                IssuedOn = issuedOn,
                ExpiredOn = expiredOn
            };


            using (var ctx = new ConXContext())
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    ctx.Tokens.Add(tokendomain);

                    ctx.SaveChanges();
                    scope.Complete();
                }
            }

            return tokendomain;

        }



        public string ValidateToken(string tokenId)
        {
            using (var ctx = new ConXContext())
            {
                //using (TransactionScope scope = new TransactionScope())
                //{
                Token token = ctx.Tokens.Where(x => x.AuthToken == tokenId).SingleOrDefault();

                if (token != null) //pass
                {
                    if (token.ExpiredOn > DateTime.Now)
                    {

                        double timeDiff = (DateTime.Now - token.ExpiredOn).TotalMinutes;
                        if (timeDiff < 10)
                        {
                            token.ExpiredOn = token.ExpiredOn.AddMinutes(10);
                            ctx.SaveChanges();
                        }

                        return "success";
                    }
                    else return "expired";
                }
                else //not found token
                {

                    return "notFound";
                }

            }

        }

        public bool Kill(string tokenId)
        {
            //_unitOfWork.TokenRepository.Delete(x => x.AuthToken == tokenId);
            //_unitOfWork.Save();
            //var isNotDeleted = _unitOfWork.TokenRepository.GetMany(x => x.AuthToken == tokenId).Any();
            //if (isNotDeleted) { return false; }



            using (var ctx = new ConXContext())
            {
                using (TransactionScope scope = new TransactionScope())
                {

                    var tokenRemove = ctx.Tokens.Where(x => x.AuthToken == tokenId).SingleOrDefault();

                    if (tokenRemove != null)
                    {
                        ctx.Tokens.Remove(tokenRemove);
                        ctx.SaveChanges();
                        scope.Complete();
                    }
                    var isNotDeleted = ctx.Tokens.Where(x => x.AuthToken == tokenId).Any();
                    if (isNotDeleted) { return false; }

                }
            }
            return true;
        }

        public Token CheckToken(string tokenId)
        {

            using (var ctx = new ConXContext())
            {
                Token ckToken = new Token();

                try

                {
                    ckToken = ctx.Tokens.Where(t => t.AuthToken == tokenId).SingleOrDefault();
                    if (ckToken != null)
                    {
                        if (ckToken.ExpiredOn < DateTime.Today)
                        {
                            throw new System.Exception("Token Activated");
                        }
                        else
                        {
                            throw new System.Exception("Token Expired");

                        }

                    }
                    else
                    {
                        throw new System.Exception("Token not found");
                    }

                }
                catch (Exception ex)
                {

                    throw new Exception(ex.Message);
                }

                return ckToken;

            }

        }

    }
}