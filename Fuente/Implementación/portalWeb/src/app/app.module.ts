import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NavegationComponent } from './navegation/navegation.component';
import { LoginComponent } from './account/login/login.component';
import { RegisterComponent } from './account/register/register.component';
import { CuentaService } from './shared/services/cuenta.service';


import { CookieService } from 'ngx-cookie-service';
import { ModalComponent } from './shared/modal/modal.component';
import { MatSidenavModule } from '@angular/material/sidenav';
import { ProductsComponent } from './products/products.component';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatStepperModule } from '@angular/material/stepper';
import { MatSelectModule } from '@angular/material/select';
import { MatPaginatorModule  } from '@angular/material/paginator';
import { MatTableModule } from '@angular/material/table';
import { OrderComponent } from './order/order.component';



@NgModule({
  declarations: [
    AppComponent,
    NavegationComponent,
    LoginComponent,
    RegisterComponent,
    ModalComponent,
    ProductsComponent,
    OrderComponent
    
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    MatSidenavModule,
    MatToolbarModule,
    MatIconModule,
    MatListModule,
    MatCardModule,
    MatButtonModule,
    MatFormFieldModule,
    MatStepperModule,
    MatSelectModule,
    MatInputModule,
    MatPaginatorModule,
    MatTableModule
  ],
  providers: [CuentaService, CookieService],
  bootstrap: [AppComponent]
})
export class AppModule { }
