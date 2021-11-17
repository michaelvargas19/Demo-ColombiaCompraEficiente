import { Component, OnInit, ViewChild } from '@angular/core';
import { ThemePalette } from '@angular/material/core';
import { MatPaginator } from '@angular/material/paginator';
import { ModalComponent } from '../shared/modal/modal.component';
import { animate, state, style, transition, trigger } from '@angular/animations';
import { MatTableDataSource } from '@angular/material/table';
import { Producto } from '../shared/model/business/producto';
import { CuentaService } from '../shared/services/cuenta.service';
import { ProductosService } from '../shared/services/productos.service';
import { MatSort } from '@angular/material/sort';
import { OrdenesService } from '../shared/services/ordenes.service';
import { OrdenCompra } from '../shared/model/business/orden-compra';
import { RequestOrdenCompra } from '../shared/model/business/request/request-orden-compra';
import { RequestBase } from '../shared/model/response-base';
import { RequestOrdenCompraDetalle } from '../shared/model/business/request/request-orden-compra-detalle';
import { Router } from '@angular/router';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css'],
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({height: '0px', minHeight: '0'})),
      state('expanded', style({height: '*'})),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ],
})
export class ProductsComponent implements OnInit {

  @ViewChild(ModalComponent,{ static: false }) modal: ModalComponent;
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort: MatSort;

  constructor(private CuentaService : CuentaService,
              private ProductosService : ProductosService,
              private OrdenesService : OrdenesService,
              private router: Router) { }

  ngOnInit() {
    this.consultarProductos();
    this.consultarCarrito();
    //this.modal.ocultarModal();
  }
  dataSource = new MatTableDataSource<Producto>();
  columnsToDisplay = ['sku', 'nombre', 'valorUnitario','nivelInventario'];  
  expandedElement: Producto | undefined;
  OrdenCompra: OrdenCompra | undefined;  
  asignaciones: Map<number,number> = new Map<number,number>();
  agregarDigitador : boolean = false;
  colorSwitch: ThemePalette = 'primary';


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
          this.dataSource = new MatTableDataSource<Producto>(data.data);
          this.dataSource.paginator = this.paginator;        
          this.dataSource.sort = this.sort;        
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

  datosCompletos(cantidad : Producto){

    return (cantidad.unidadesOrden > 0);
  }


  existeCarrito(){

    return (this.OrdenCompra != undefined);
  }

  crearCarrito(){

    //this.modal.mostraCargando();

    let request = new RequestOrdenCompra();
    request.idUsuarioCrea = this.CuentaService.getIdUsuario();
    
    let requestDTO = new RequestBase<RequestOrdenCompra>();
    requestDTO.data = request;
    requestDTO.usuario = this.CuentaService.getUserName();

    this.OrdenesService.crearCarrito(requestDTO).subscribe(
      data => {
      
        if( data.code == 200 ){
          this.OrdenCompra = data.data;
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


  agregarACarrito(element: Producto){


    //this.modal.mostraCargando();

    if(this.existeCarrito()){

      let request = new RequestOrdenCompraDetalle();
      request.idUsuario = this.CuentaService.getIdUsuario();
      request.idOrden = (this.OrdenCompra!=undefined)? this.OrdenCompra.idOrden: 0;
      request.idProducto = element.id;
      request.cantidad = element.unidadesOrden;
      
      let requestDTO = new RequestBase<RequestOrdenCompraDetalle>();
      requestDTO.data = request;
      requestDTO.usuario = this.CuentaService.getUserName();

      this.OrdenesService.agregarDetalleACarrito(requestDTO).subscribe(
        data => {
        
          if( data.code == 200 ){
            this.OrdenCompra = data.data;
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


  verCarrito(){
    this.router.navigate(['/ordenCompra']);
  }

  estaAutenticado(){
    return this.CuentaService.isAuthenticated();
  }

}
