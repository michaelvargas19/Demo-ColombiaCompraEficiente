import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.prod';
import { Producto } from '../model/business/producto';
import { ResponseBase } from '../model/resquest-base';
import { CuentaService } from './cuenta.service';

@Injectable({
  providedIn: 'root'
})
export class ProductosService {

  constructor(
    private http: HttpClient,
    private cookies: CookieService,
    private CuentaService: CuentaService,
    private router: Router) {
  }
  
  
  private token:string="";

  getToken() {   
    this.token=this.CuentaService.getToken();   
    return this.token;
  }

  private URL_Productos: string = environment.URL_CATALOGOS+ '/api/Productos';

  getProductos(): Observable<ResponseBase<Producto[]>> {  

    return this.http.get<ResponseBase<Producto[]>>(this.URL_Productos + "?skip=0&take=200", { 
      headers:{
        Authorization:'Bearer '+this.getToken()
      }
    });
  }

}
