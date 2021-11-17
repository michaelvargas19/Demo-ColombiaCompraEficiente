import { RoleQuery } from "./rolequery";

export class UserQuery {

    public idUsuario : number;
    public usuario : string;
    public email : string;
    public nombres : string
    public apellidos : string
    public identificacion : string
    public telefonoMovil : string
    public idTipoAut : number;
    public tipoAutenticacion : string;
    public organizacion	 : string;
    public cargo : string;
    public description : string;
    public esExterno : boolean;
    public roles : RoleQuery [] = [];
}
