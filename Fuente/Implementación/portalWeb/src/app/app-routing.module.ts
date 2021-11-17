import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './account/login/login.component';
import { RegisterComponent } from './account/register/register.component';
import { NavegationComponent } from './navegation/navegation.component';
import { OrderComponent } from './order/order.component';
import { ProductsComponent } from './products/products.component';

const routes: Routes = [
  { path: '', component: NavegationComponent,
    children: [
      { path: 'login', component: LoginComponent },
      { path: 'productos', component: ProductsComponent },
      { path: 'registro', component: RegisterComponent },
      { path: 'ordenCompra', component: OrderComponent },
      
    ]
  },
  { path: '**', redirectTo: '/productos' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppRoutingModule { }
