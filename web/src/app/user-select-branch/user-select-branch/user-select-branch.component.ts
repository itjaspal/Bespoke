import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../../_service/authentication.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-user-select-branch',
  templateUrl: './user-select-branch.component.html',
  styleUrls: ['./user-select-branch.component.scss']
})
export class UserSelectBranchComponent implements OnInit {

  public user;

  constructor(
    private authenticationService: AuthenticationService,
    private router: Router,
  ) { }

  async ngOnInit() {
    this.user = this.authenticationService.getLoginUser();

    if (this.user.userBranchPrvlgList.length == 1) {
      this.select(this.user.userBranchPrvlgList[0]);
    } else if (this.user.userBranchPrvlgList.length == 0) {
      this.router.navigateByUrl('/app/home');
    }
  }

  async select(branch) {

    let res: any = await this.authenticationService.getPcUserRole(branch);

    // this.router.navigateByUrl('/app/mobile-navigator');
    this.router.navigateByUrl('/app/home');

  }

  logout() {
    this.authenticationService.logout();
  }

}
