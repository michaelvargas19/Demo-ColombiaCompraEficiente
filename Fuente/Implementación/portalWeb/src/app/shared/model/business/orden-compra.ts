import { OrdenCompraDetalle } from "./orden-compra-detalle";

export class OrdenCompra {

    public idOrden:number;
    public fechaCreacion:Date;
    public idUsuarioCrea:number;
    public idEstado:number;
    public valorTotal:number;

    public detalle :OrdenCompraDetalle[];

}
