import { TokenJWT } from "../token-jwt";
import { UserQuery } from "./user-query";

export class LoginResponse {

    public autenticado : boolean;
    public tokenJWT : TokenJWT;
    public idAplicacion : string;
    public token : string;
    public tokenValido : boolean;
    public mensaje : string;
    public bloqueado : boolean;
    public urLdesbloqueo : string
    
    public datosUsuario : UserQuery;
}

