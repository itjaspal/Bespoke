import { AuthenGuard } from './../_service/authen.guard';
import { HomeComponent } from './../home/home.component';
import { DepartmentSearchComponent } from './../master-department/department-search/department-search.component';
import { DepartmentCreateComponent } from './../master-department/department-create/department-create.component';
import { DepartmentUpdateComponent } from './../master-department/department-update/department-update.component';
import { DepartmentViewComponent } from './../master-department/department-view/department-view.component';
import { BranchGroupSearchComponent } from './../master-branch-group/branch-group-search/branch-group-search.component';
import { BranchGroupCreateComponent } from './../master-branch-group/branch-group-create/branch-group-create.component';
import { BranchGroupUpdateComponent } from './../master-branch-group/branch-group-update/branch-group-update.component';
import { BranchGroupViewComponent } from './../master-branch-group/branch-group-view/branch-group-view.component';
import { RouterModule, Routes } from '@angular/router';

import { LayoutComponent } from './layout.component';

import { MobileMenuComponent } from '../mobile-menu/mobile-menu.component';

import { BranchSaveComponent } from './../master-branch/branch-save/branch-save.component';
import { BranchViewComponent } from './../master-branch/branch-view/branch-view.component';
import { BranchSearchComponent } from './../master-branch/branch-search/branch-search.component';
import { MobileProfileComponent } from '../mobile-profile/mobile-profile.component';
import { InqUserComponent } from '../master-user/inq/inq-user.component';
import { CreateUserComponent } from '../master-user/create/create-user.component';
import { ViewUserComponent } from '../master-user/view/view-user.component';
import { UpdateUserComponent } from '../master-user/update/update-user.component';
import { ResetPasswordUserComponent } from '../master-user/reset-password/reset-user.component';
import { InqUserRoleComponent } from '../master-user-role/inq/inq-user-role.component';
import { CreateUserRoleComponent } from '../master-user-role/create/create-user-role.component';
import { ViewUserRoleComponent } from '../master-user-role/view/view-user-role.component';
import { UpdateUserRoleComponent } from '../master-user-role/update/update-user-role.component';

import { ChangePasswordComponent } from '../master-user/change-password/change-password.component';
import { MenuGroupSearchComponent } from '../master-menu-group/menu-group-search/menu-group-search.component';
import { MenuGroupCreateComponent } from '../master-menu-group/menu-group-create/menu-group-create.component';
import { MenuGroupUpdateComponent } from '../master-menu-group/menu-group-update/menu-group-update.component';
import { MenuGroupViewComponent } from '../master-menu-group/menu-group-view/menu-group-view.component';
import { MenuSearchComponent } from '../master-menu/menu-search/menu-search.component';
import { MenuViewComponent } from '../master-menu/menu-view/menu-view.component';
import { MenuCreateComponent } from '../master-menu/menu-create/menu-create.component';
import { MenuUpdateComponent } from '../master-menu/menu-update/menu-update.component';
import { SalesSearchComponent } from '../sales/sales-search/sales-search.component';
import { SalesAddComponent } from '../sales/sales-add/sales-add.component';
import { SalesViewComponent } from '../sales/sales-view/sales-view.component';
import { SalesEditComponent } from '../sales/sales-edit/sales-edit.component';
import { SalesDesignComponent } from '../sales/sales-design/sales-design.component';
import { SalesProductComponent } from '../sales/sales-product/sales-product.component';
import { SalesSummaryComponent } from '../sales/sales-summary/sales-summary.component';
import { TrackSearchComponent } from '../sales-track/track-search/track-search.component';
import { TrackViewComponent } from '../sales-track/track-view/track-view.component';
import { ColorFontSearchComponent } from '../master-color-font/color-font-search/color-font-search.component';
import { ColorFontUpdateComponent } from '../master-color-font/color-font-update/color-font-update.component';
import { EmbroideryUpdateComponent } from '../master-embroidery/embroidery-update/embroidery-update.component';
import { EmbroiderySearchComponent } from '../master-embroidery/embroidery-search/embroidery-search.component';
import { CustomerSearchComponent } from '../customer/customer-search/customer-search.component';
import { CustomerCreateComponent } from '../customer/customer-create/customer-create.component';
import { CatalogDesignSearchComponent } from '../master-catalog-design/catalog-design-search/catalog-design-search.component';
import { CatalogDesignCreateComponent } from '../master-catalog-design/catalog-design-create/catalog-design-create.component';
import { CatalogDesignUpdateComponent } from '../master-catalog-design/catalog-design-update/catalog-design-update.component';
import { CatalogDesignViewComponent } from '../master-catalog-design/catalog-design-view/catalog-design-view.component';
import { CatalogColorSearchComponent } from '../master-catalog-color/catalog-color-search/catalog-color-search.component';
import { CatalogColorCreateComponent } from '../master-catalog-color/catalog-color-create/catalog-color-create.component';
import { CatalogColorUpdateComponent } from '../master-catalog-color/catalog-color-update/catalog-color-update.component';
import { CatalogEmbColorSearchComponent } from '../master-catalog-emb-color/catalog-emb-color-search/catalog-emb-color-search.component';
import { CatalogEmbColorCreateComponent } from '../master-catalog-emb-color/catalog-emb-color-create/catalog-emb-color-create.component';
import { CatalogBorderColorSearchComponent } from '../master-catalog-border-color/catalog-border-color-search/catalog-border-color-search.component';
import { CatalogBorderColorCreateComponent } from '../master-catalog-border-color/catalog-border-color-create/catalog-border-color-create.component';
import { CatalogTypeSearchComponent } from '../master-catalog-type/catalog-type-search/catalog-type-search.component';
import { CatalogTypeCreateComponent } from '../master-catalog-type/catalog-type-create/catalog-type-create.component';
import { CatalogSizeSearchComponent } from '../master-catalog-size/catalog-size-search/catalog-size-search.component';
import { CatalogSizeCreateComponent } from '../master-catalog-size/catalog-size-create/catalog-size-create.component';

 

const routes: Routes = [
  {
    path: 'app',
    component: LayoutComponent,
    children: [
      
      { path: 'home', component: HomeComponent, canActivate: [AuthenGuard] },
      { path: 'mobile-navigator', component: MobileMenuComponent, canActivate: [AuthenGuard] },
      { path: 'mobile-profile', component: MobileProfileComponent, canActivate: [AuthenGuard] },

      //branch
      { path: 'branch', component: BranchSearchComponent, canActivate: [AuthenGuard], data: { parentUrl: "/app/branch" } },
      { path: 'branch/add/:branchGroupId', component: BranchSaveComponent, canActivate: [AuthenGuard], data: { parentUrl: "/app/branch" } },
      { path: 'branch/view/:branchId', component: BranchViewComponent, canActivate: [AuthenGuard], data: { parentUrl: "/app/branch" } },
      { path: 'branch/edit/:branchId', component: BranchSaveComponent, canActivate: [AuthenGuard], data: { parentUrl: "/app/branch" } },
      

      //branch group
      { path: 'branch-group', component: BranchGroupSearchComponent, canActivate: [AuthenGuard], data: { parentUrl: "/app/branch-group" } },
      { path: 'branch-group/create', component: BranchGroupCreateComponent, canActivate: [AuthenGuard], data: { parentUrl: "/app/branch-group" } },
      { path: 'branch-group/update/:id', component: BranchGroupUpdateComponent, canActivate: [AuthenGuard], data: { parentUrl: "/app/branch-group" } },
      { path: 'branch-group/view/:id', component: BranchGroupViewComponent, canActivate: [AuthenGuard], data: { parentUrl: "/app/branch-group" } },

      //department
      { path: 'department', component: DepartmentSearchComponent, canActivate: [AuthenGuard], data: { parentUrl: "/app/department" } },
      { path: 'department/create', component: DepartmentCreateComponent, canActivate: [AuthenGuard], data: { parentUrl: "/app/department" } },
      { path: 'department/update/:departmentId', component: DepartmentUpdateComponent, canActivate: [AuthenGuard], data: { parentUrl: "/app/department" } },
      { path: 'department/view/:departmentId', component: DepartmentViewComponent, canActivate: [AuthenGuard], data: { parentUrl: "/app/department" } },

      //User
      { path: 'user', component: InqUserComponent, canActivate: [AuthenGuard], data: { parentUrl: "/app/user" } },
      { path: 'user/create', component: CreateUserComponent, canActivate: [AuthenGuard], data: { parentUrl: "/app/user" } },
      { path: 'user/view/:id', component: ViewUserComponent, canActivate: [AuthenGuard], data: { parentUrl: "/app/user" } },
      { path: 'user/update/:id', component: UpdateUserComponent, canActivate: [AuthenGuard], data: { parentUrl: "/app/user" } },
      { path: 'user/reset-password/:id', component: ResetPasswordUserComponent, canActivate: [AuthenGuard], data: { parentUrl: "/app/user" } },
      { path: 'user/change-password', component: ChangePasswordComponent },

      //User Role
      { path: 'user-role', component: InqUserRoleComponent, canActivate: [AuthenGuard], data: { parentUrl: "/app/user-role" } },
      { path: 'user-role/create', component: CreateUserRoleComponent, canActivate: [AuthenGuard], data: { parentUrl: "/app/user-role" } },
      { path: 'user-role/view/:id', component: ViewUserRoleComponent, canActivate: [AuthenGuard], data: { parentUrl: "/app/user-role" } },
      { path: 'user-role/update/:id', component: UpdateUserRoleComponent, canActivate: [AuthenGuard], data: { parentUrl: "/app/user-role" } },

      
      //menu-group
      { path: 'menu-group', component: MenuGroupSearchComponent, canActivate: [AuthenGuard], data: { parentUrl: "/app/menu-group" } },
       { path: 'menu-group/create', component: MenuGroupCreateComponent, canActivate: [AuthenGuard], data: { parentUrl: "/app/menu-group" } },
       { path: 'menu-group/update/:menuFunctionGroupId', component: MenuGroupUpdateComponent, canActivate: [AuthenGuard], data: { parentUrl: "/app/menu-group" } },
       { path: 'menu-group/view/:menuFunctionGroupId', component: MenuGroupViewComponent, canActivate: [AuthenGuard], data: { parentUrl: "/app/menu-group" } },

      //menu
      { path: 'menu', component: MenuSearchComponent, canActivate: [AuthenGuard], data: { parentUrl: "/app/menu" } },
      { path: 'menu/create', component: MenuCreateComponent, canActivate: [AuthenGuard], data: { parentUrl: "/app/menu" } },
      { path: 'menu/update/:menuFunctionId', component: MenuUpdateComponent, canActivate: [AuthenGuard], data: { parentUrl: "/app/menu" } },
      { path: 'menu/view/:menuFunctionId', component: MenuViewComponent, canActivate: [AuthenGuard], data: { parentUrl: "/app/menu" } },

      //Master Color of Font
      { path: "color-font", component: ColorFontSearchComponent, canActivate: [AuthenGuard], data: { parentUrl: "/app/color-font" } },
      { path: "color-font/update/:id", component: ColorFontUpdateComponent, canActivate: [AuthenGuard], data: { parentUrl: "/app/color-font" } },
      
      //Master Emb Mast
      { path: "emb-mast", component: EmbroiderySearchComponent, canActivate: [AuthenGuard], data: { parentUrl: "/app/emb-mast" } },
      { path: "emb-mast/update/:id", component: EmbroideryUpdateComponent, canActivate: [AuthenGuard], data: { parentUrl: "/app/emb-mast" } },
        
      //Master Customer
      { path: "customer", component: CustomerSearchComponent, canActivate: [AuthenGuard], data: { parentUrl: "/app/customer" } },
      { path: "customer/create", component: CustomerCreateComponent, canActivate: [AuthenGuard], data: { parentUrl: "/app/customer" } },
      
      //Master Catalog Design
      { path: "catalog", component: CatalogDesignSearchComponent, canActivate: [AuthenGuard], data: { parentUrl: "/app/catalog" } },
      { path: "catalog/create", component: CatalogDesignCreateComponent, canActivate: [AuthenGuard], data: { parentUrl: "/app/catalog" } },
      { path: "catalog/update/:id", component: CatalogDesignUpdateComponent, canActivate: [AuthenGuard], data: { parentUrl: "/app/catalog" } },
      { path: "catalog/view/:id", component: CatalogDesignViewComponent, canActivate: [AuthenGuard], data: { parentUrl: "/app/catalog" } },

      //Master Catalog Color
      { path: "catalog-color/:catalog_id", component: CatalogColorSearchComponent, canActivate: [AuthenGuard], data: { parentUrl: "/app/catalog-color" } },
      { path: "catalog-color/:catalog_id/create", component: CatalogColorCreateComponent, canActivate: [AuthenGuard], data: { parentUrl: "/app/catalog-color" } },
      //{ path: "catalog-color/update/:id", component: CatalogColorUpdateComponent, canActivate: [AuthenGuard], data: { parentUrl: "/app/catalog-color" } },

      //Master Catalog Embroidery
      { path: "catalog-emb/:catalog_id", component: CatalogEmbColorSearchComponent, canActivate: [AuthenGuard], data: { parentUrl: "/app/catalog-emb" } },
      { path: "catalog-emb/:catalog_id/create", component: CatalogEmbColorCreateComponent, canActivate: [AuthenGuard], data: { parentUrl: "/app/catalog-emb" } },
      //{ path: "catalog-emb/update/:id", component: CatalogEmbUpdateComponent, canActivate: [AuthenGuard], data: { parentUrl: "/app/catalog-emb" } },

      //Master Catalog Border
      { path: "catalog-border/:catalog_id", component: CatalogBorderColorSearchComponent, canActivate: [AuthenGuard], data: { parentUrl: "/app/catalog-border" } },
      { path: "catalog-border/:catalog_id/create", component: CatalogBorderColorCreateComponent, canActivate: [AuthenGuard], data: { parentUrl: "/app/catalog-border" } },
      //{ path: "catalog-border/update/:id", component: CatalogBorderUpdateComponent, canActivate: [AuthenGuard], data: { parentUrl: "/app/catalog-emb" } },


      //Master Catalog Type
      { path: "catalog-type/:catalog_id", component: CatalogTypeSearchComponent, canActivate: [AuthenGuard], data: { parentUrl: "/app/catalog-type" } },
      { path: "catalog-type/:catalog_id/create", component: CatalogTypeCreateComponent, canActivate: [AuthenGuard], data: { parentUrl: "/app/catalog-type" } },
      //{ path: "catalog-border/update/:id", component: CatalogTypeUpdateComponent, canActivate: [AuthenGuard], data: { parentUrl: "/app/catalog-emb" } },


      //Master Catalog Size
      { path: "catalog-size/:catalog_id", component: CatalogSizeSearchComponent, canActivate: [AuthenGuard], data: { parentUrl: "/app/catalog-size" } },
      { path: "catalog-size/:catalog_id/create", component: CatalogSizeCreateComponent, canActivate: [AuthenGuard], data: { parentUrl: "/app/catalog-size" } },
      //{ path: "catalog-border/update/:id", component: CatalogTypeUpdateComponent, canActivate: [AuthenGuard], data: { parentUrl: "/app/catalog-emb" } },



      //Sales
      { path: "sale", component: SalesSearchComponent, canActivate: [AuthenGuard], data: { parentUrl: "/app/sale" } },
      { path: "sale/create", component: SalesAddComponent, canActivate: [AuthenGuard], data: { parentUrl: "/app/sale" } },
      { path: "sale/view/:id", component: SalesViewComponent, canActivate: [AuthenGuard], data: { parentUrl: "/app/sale" } },
      { path: "sale/update/:id", component: SalesEditComponent, canActivate: [AuthenGuard], data: { parentUrl: "/app/sale" } },
      { path: "sale/design", component: SalesDesignComponent, canActivate: [AuthenGuard], data: { parentUrl: "/app/sale" } },
      { path: "sale/product", component: SalesProductComponent, canActivate: [AuthenGuard], data: { parentUrl: "/app/sale" } },
      { path: "sale/summary", component: SalesSummaryComponent, canActivate: [AuthenGuard], data: { parentUrl: "/app/sale" } },
      
      //Tracking
      { path: "track", component: TrackSearchComponent, canActivate: [AuthenGuard], data: { parentUrl: "/app/track" } },
      { path: "track/view/:id", component: TrackViewComponent, canActivate: [AuthenGuard], data: { parentUrl: "/app/track" } },

       { path: '**', redirectTo: 'home', pathMatch: 'full' },
    ]
  }
];

export const LayoutRoutingModule = RouterModule.forChild(routes);
