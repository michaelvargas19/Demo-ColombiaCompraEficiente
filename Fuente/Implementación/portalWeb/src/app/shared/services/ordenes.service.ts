import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.prod';
import { OrdenCompra } from '../model/business/orden-compra';
import { RequestOrdenCompra } from '../model/business/request/request-orden-compra';
import { RequestOrdenCompraDetalle } from '../model/business/request/request-orden-compra-detalle';
import { RequestBase } from '../model/response-base';
import { ResponseBase } from '../model/resquest-base';
import { CuentaService } from './cuenta.service';

@Injectable({
  providedIn: 'root'
})
export class OrdenesService {

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

  private URL_ORDENES: string = environment.URL_ORDENES+ '/api/Ordenes';

  getCarrito(): Observable<ResponseBase<OrdenCompra>> {  

    return this.http.get<ResponseBase<OrdenCompra>>(this.URL_ORDENES + "/"+this.CuentaService.getIdUsuario(), { 
      headers:{
        Authorization:'Bearer '+this.getToken()
      }
    });
  }


  crearCarrito(request : RequestBase<RequestOrdenCompra>): Observable<ResponseBase<OrdenCompra>> {  

    return this.http.post<ResponseBase<OrdenCompra>>(this.URL_ORDENES , request, { 
      headers:{
        Authorization:'Bearer '+this.getToken()
      }
    });
  }


  agregarDetalleACarrito(request : RequestBase<RequestOrdenCompraDetalle>): Observable<ResponseBase<OrdenCompra>> {  

    return this.http.post<ResponseBase<OrdenCompra>>(this.URL_ORDENES+"/detalle" , request, { 
      headers:{
        Authorization:'Bearer '+this.getToken()
      }
    });
  }


  eliminarDetalleACarrito(request : RequestBase<RequestOrdenCompraDetalle>): Observable<ResponseBase<OrdenCompra>> {  

    let uri = this.URL_ORDENES+"/detalle";
    let options = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': "Bearer " + this.getToken()
      }),
      body: request
    }

    return this.http.delete<ResponseBase<OrdenCompra>>(uri, options);


  }


}
