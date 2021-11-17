import { Component, OnInit, ViewChild } from '@angular/core';
import { ThemePalette } from '@angular/material/core';
import { MatPaginator } from '@angular/material/paginator';
import { animate, state, style, transition, trigger } from '@angular/animations';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { ModalComponent } from '../shared/modal/modal.component';
import { OrdenCompra } from '../shared/model/business/orden-compra';
import { OrdenCompraDetalle } from '../shared/model/business/orden-compra-detalle';
import { Producto } from '../shared/model/business/producto';
import { CuentaService } from '../shared/services/cuenta.service';
import { OrdenesService } from '../shared/services/ordenes.service';
import { ProductosService } from '../shared/services/productos.service';
import { RequestOrdenCompraDetalle } from '../shared/model/business/request/request-orden-compra-detalle';
import { RequestBase } from '../shared/model/response-base';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.css'],
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({height: '0px', minHeight: '0'})),
      state('expanded', style({height: '*'})),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ],
})
export class OrderComponent implements OnInit {

  @ViewChild(ModalComponent,{ static: false }) modal: ModalComponent;
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort: MatSort;

  constructor(private CuentaService : CuentaService,
              private ProductosService : ProductosService,
              private OrdenesService : OrdenesService,
              private router: Router) { }

  ngOnInit() {
    this.consultarCarrito();
    this.consultarProductos();
    //this.modal.ocultarModal();
  }
  dataSource = new MatTableDataSource<OrdenCompraDetalle>();
  columnsToDisplay = ['idProducto', 'idOrden', 'cantidad'];  
  expandedElement: Producto | undefined;
  OrdenCompra: OrdenCompra | undefined;  
  asignaciones: Map<number,number> = new Map<number,number>();
  agregarDigitador : boolean = false;
  colorSwitch: ThemePalette = 'primary';
  listasProductos: Producto[];

  filtrarTabla(event: Event){
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }


  consultarProductos(){
    
    //this.modal.mostraCargando();

    //Listas Generales
    this.ProductosService.getProductos().subscribe(
      data => {
      
        if( data.code == 200 ){
          let pro = data.data;    
          this.listasProductos = pro;
        }else{
          this.modal.modalGeneral("Productos", data.message, "error");  
        }
        

      },
      error => {
        this.modal.ocultarModal();
        this.modal.modalGeneral("Productos", error.error.message, "error");  

        if (error.status==401){
          this.CuentaService.logout();
        }
      }
    );

  }


  consultarCarrito(){
    
    //this.modal.mostraCargando();

    
    this.OrdenesService.getCarrito().subscribe(
      data => {
      
        if( data.code == 200 ){
          
          this.OrdenCompra = data.data;
          this.dataSource = new MatTableDataSource<OrdenCompraDetalle>(data.data.detalle);
          this.dataSource.paginator = this.paginator;        
          this.dataSource.sort = this.sort;   

          
        }else{
          this.modal.modalGeneral("Carrito", data.message, "error");  
        }
        

      },
      error => {
        this.modal.ocultarModal();
        this.modal.modalGeneral("Carrito", error.error.message, "error");  

        if (error.status==401){
          this.CuentaService.logout();
        }
      }
    );

    
  }


  getNombreProducto(idProducto : number){
    let pro = this.listasProductos.find(l=> l.id == idProducto);
    return (pro != undefined)? pro.nombre : "";
  }


  eliminarProducto(element: OrdenCompraDetalle){
    
    //this.modal.mostraCargando();

    let request = new RequestOrdenCompraDetalle();
      request.idUsuario = this.CuentaService.getIdUsuario();
      request.idOrden = element.idOrden;
      request.idProducto = element.idProducto;
      request.cantidad = element.cantidad;

      let requestDTO = new RequestBase<RequestOrdenCompraDetalle>();
      requestDTO.data = request;
      requestDTO.usuario = this.CuentaService.getUserName();
    
    this.OrdenesService.eliminarDetalleACarrito(requestDTO).subscribe(
      data => {
      
        if( data.code == 200 ){
          
          this.OrdenCompra = data.data;
          this.dataSource = new MatTableDataSource<OrdenCompraDetalle>(data.data.detalle);
          this.dataSource.paginator = this.paginator;        
          this.dataSource.sort = this.sort;   
          
          this.modal.modalGeneral("Carrito", data.message, "success"); 
          
        }else{
          this.modal.modalGeneral("Carrito", data.message, "error");  
        }
        

      },
      error => {
        this.modal.ocultarModal();
        this.modal.modalGeneral("Carrito", error.error.message, "error");  

        if (error.status==401){
          this.CuentaService.logout();
        }
      }
    );


  }
}
