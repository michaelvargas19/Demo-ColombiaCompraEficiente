import { AuthTypeQuery } from "../query/auth-type-query";
import { RoleQuery } from "../query/rolequery";
import { TypeDocument } from "../query/type-document";


export class ConfigNewUser {

    public tiposAutenticacion : AuthTypeQuery[] = [];

    public tiposDocumento : TypeDocument[] = [];

    public roles : RoleQuery[] = [];

}