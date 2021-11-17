import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { LoginResponse } from '../model/account/query/login-response';
import { environment } from 'src/environments/environment.prod';
import { Credenciales } from '../model/account/credenciales';
import { CookieService } from 'ngx-cookie-service';
import { Router } from '@angular/router';
import { ResponseBase } from '../model/resquest-base';
import { RequestBase } from '../model/response-base';
import { UserCmd } from '../model/account/command/user-cmd';
import { UserQuery } from '../model/account/query/user-query';
import { TokenJWT } from '../model/account/token-jwt';
import { ConfigNewUser } from '../model/account/command/config-new-user';

@Injectable({
  providedIn: 'root'
})
export class CuentaService {


  private URL_LOGIN: string = environment.URL_LOGIN;
  private ID_APPLICACION: string = environment.ID_APPLICACION;

  usuario: string;
  idUsuario: number;
  userName: string;
  token: string;
  rol: string[];

  constructor(
    private http: HttpClient,
    private cookies: CookieService,
    private router: Router) {
  }

  authenticate(credenciales: Credenciales) {
    credenciales.idAplicacion = this.ID_APPLICACION;
    return this.http.post<ResponseBase<LoginResponse>>(this.URL_LOGIN + '/api/Sesiones' , credenciales);

}

getRoles(): string[]{
  
  this.rol=[];
  this.rol=this.cookies.get("rol").split(';');  
  
  return this.rol;
}

getUsuario() {
  this.usuario = "";
  this.usuario = this.cookies.get("usuario");    

  return this.usuario;
}

getIdUsuario() {
  this.idUsuario = parseInt(this.cookies.get("idUsuario"));
  return this.idUsuario;
}

getUserName() {
  this.userName = "";
  this.userName = this.cookies.get("username");    

  return this.userName;
}

getToken() {
  this.token = "";
  this.token = this.cookies.get("token");    

  return this.token;
}

getGenericToken() {
  
  let genericToken = environment.GENERIC_TOKEN;

  return genericToken;
}

cargarUsuario(username: string, authInfo: LoginResponse) {
  
  this.cookies.deleteAll();
  
  let roles = "";

    authInfo.datosUsuario.roles.forEach(r => {
      if(roles != "" ){
        roles = roles + ";";
      }
      roles = roles + r.nombre;
    });

  this.cookies.set("autenticado", "ok");
  this.cookies.set("usuario", authInfo.datosUsuario.nombres + " " + authInfo.datosUsuario.apellidos);
  this.cookies.set("username", username);
  this.cookies.set("roles", roles);
  this.cookies.set("idUsuario", authInfo.datosUsuario.idUsuario.toString());
  this.cookies.set("token", authInfo.tokenJWT.token);

  return true;

}

isAuthenticated() {
  
  var autenticado = this.cookies.get("autenticado");  
  var usuario = this.cookies.get("usuario");  
  var username = this.cookies.get("username");  
  var roles = this.cookies.get("roles");  
  var token = this.cookies.get("token");  

  
  return (autenticado == "ok") && (usuario != "") && (username != "") && (roles != "") && (token != "");

}

logout():boolean {
  debugger;
  this.cookies.deleteAll();
  this.router.navigate(['/productos']);
  
  return true;
  
}



register(request: RequestBase<UserCmd>) {
  return this.http.post<ResponseBase<UserQuery>>(this.URL_LOGIN + '/api/Usuarios' , request);

}


getConfigurationNewUsers() {
  
  let token = new TokenJWT();
  token.idAplicacion = environment.ID_APPLICACION;
  token.token = this.getGenericToken();
  
  if(token.token == ""){
    this.logout();
  }

  return this.http.post<ResponseBase<ConfigNewUser>>(this.URL_LOGIN + '/api/Usuarios/configuracion', token);

}


}
