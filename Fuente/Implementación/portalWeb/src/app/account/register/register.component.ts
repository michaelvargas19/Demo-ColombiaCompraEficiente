import { MediaMatcher } from '@angular/cdk/layout';
import { ChangeDetectorRef, Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ModalComponent } from 'src/app/shared/modal/modal.component';
import { UserCmd } from 'src/app/shared/model/account/command/user-cmd';
import { AuthTypeQuery } from 'src/app/shared/model/account/query/auth-type-query';
import { RoleQuery } from 'src/app/shared/model/account/query/rolequery';
import { TypeDocument } from 'src/app/shared/model/account/query/type-document';
import { UserQuery } from 'src/app/shared/model/account/query/user-query';
import { RequestBase } from 'src/app/shared/model/response-base';
import { ResponseBase } from 'src/app/shared/model/resquest-base';
import { CuentaService } from 'src/app/shared/services/cuenta.service';
import { environment } from 'src/environments/environment.prod';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  @ViewChild(ModalComponent,{ static: false }) modal: ModalComponent;

  private _mobileQueryListener: () => void;
  mobileQuery: MediaQueryList;

  roleToRegister : RoleQuery | undefined;
  AuthType : AuthTypeQuery | undefined;
  fechaActual : Date = new Date();

  listTypeDocument: TypeDocument[]=[];
  listAuthTypeQuery: AuthTypeQuery[]=[];
  listRoles: RoleQuery[]=[];

  datosBasicos = new FormGroup({
    primerNombre: new FormControl('', Validators.required),
    segundoNombre: new FormControl(''),
    primerApellido: new FormControl('', Validators.required),
    segundoApellido: new FormControl('', Validators.required),
    tipoDocumento: new FormControl(''),
    identificacion: new FormControl(''),
    descripcion: new FormControl('', Validators.required)
  });

  datosCuenta = new FormGroup({
    usuario: new FormControl('', Validators.required),
    email: new FormControl('', [ Validators.required, Validators.email ] ),
    contrasena: new FormControl('', [ Validators.required, Validators.minLength(6) ] )
  })

   

  constructor(private _formBuilder: FormBuilder,
    private router: Router, private CuentaService :CuentaService,
    changeDetectorRef: ChangeDetectorRef, media: MediaMatcher) {
      this.modal= new ModalComponent();
      this.mobileQuery = media.matchMedia('(max-width: 1200px)');
      this._mobileQueryListener = () => changeDetectorRef.detectChanges();
      this.mobileQuery.addListener(this._mobileQueryListener); 

    }

  ngOnInit() {
  this.cargarListas();
  }


  //Cargar listas desplegables
  private cargarListas(){
    
    let done = true;

    this.modal.mostraCargando();

    this.fechaActual = new Date();

    //Tipos de Documento
    this.CuentaService.getConfigurationNewUsers().subscribe(
      data => {
        let c = data.data;    
        this.listTypeDocument = c.tiposDocumento;
        this.listAuthTypeQuery = c.tiposAutenticacion;
        this.listRoles = c.roles;


        this.AuthType = this.listAuthTypeQuery.find(a=> a.autenticacion == "Contraseña");
        
        //this.AuthType = this.listAuthTypeQuery.find(a=> a.autenticacion == "Contraseña");
        this.roleToRegister = this.listRoles.find(r=> r.nombre == "Proveedor");

      },
      error => {
        done = false;
        if (error.status==401){
          this.router.navigate(['/login']);
        }
      }
    );



    let auxModal = this.modal;
    setTimeout(function(){
      if(!done){
        auxModal.ocultarModal();
        auxModal.modalGeneral("Crear Cuenta", "No ha sido posible configurar el formulario de registro. Intentelo de nuevo.", "error");
      }else{
        auxModal.ocultarModal();
      }
    },2000);

  }


  AddUser(){
    let request = new UserCmd();
    let dataCompleted = true;

      if( (this.datosCuenta.get("contrasena") == null)  )
          {
            dataCompleted = false;
      }else{

        //Se valida la coincidencia de las contraseñas
        if(true){
            request.contrasena = this.datosCuenta.get("contrasena")?.value;
        }

      }

      //Datos de Cuenta
      if(this.datosCuenta.get("usuario") != null){
        request.usuario = this.datosCuenta.get("usuario")?.value;
      }

      if(this.datosCuenta.get("email") != null){
        request.email = this.datosCuenta.get("email")?.value;
      }




      //Datos Básicos
      if(this.datosBasicos.get("primerNombre") != null){
        request.primerNombre = this.datosBasicos.get("primerNombre")?.value;
      }

      if(this.datosBasicos.get("segundoNombre") != null){
        request.segundoNombre = this.datosBasicos.get("segundoNombre")?.value;
      }else{
        request.segundoNombre = "";
      }


      if(this.datosBasicos.get("primerApellido") != null){
        request.primerApellido = this.datosBasicos.get("primerApellido")?.value;
      }


      if(this.datosBasicos.get("segundoApellido") != null){
        request.segundoApellido = this.datosBasicos.get("segundoApellido")?.value;
      }

      if(this.datosBasicos.get("tipoDocumento") != null){
        request.idTipoDocumento = this.datosBasicos.get("tipoDocumento")?.value.idTipo;
      }else{
        request.idTipoDocumento = 0;
      }
      
      if(this.datosBasicos.get("identificacion") != null){
        request.identificacion = this.datosBasicos.get("identificacion")?.value;
      }else{
        request.identificacion = "";
      }


      //Datos de accesibilidad
      if(this.roleToRegister != null){
        request.cargo = this.roleToRegister.nombre;
        request.idRole = this.roleToRegister.id;
        request.idAplicacion = environment.ID_APPLICACION;
      }else{    
        dataCompleted = false;
      }


      if(this.AuthType != null){
        request.idTipoAuth = this.AuthType.idTipo;
      }else{
        dataCompleted = false;
      }

      
      request.organizacion = environment.ORGANIZATION;
      request.esExterno = false;
      request.descripcion = "Usuario creado desdel el portal web. Fecha: "+ (new Date()).toString();
      request.idAplicacion = environment.ID_APPLICACION;

      let requestBase = new RequestBase<UserCmd>();
      requestBase.fecha = new Date();
      requestBase.usuario = request.usuario;
      requestBase.data = request;


      if(dataCompleted){

      this.CuentaService.register(requestBase).subscribe((res: ResponseBase<UserQuery>) => {
      
        if (res.code == 200) {
          
          this.modal.modalGeneralTimer("Crear cuenta", (res.data==undefined)?  res.message : "La transacción se ha completado correctamente.", "success",2000);
          this.router.navigate(['/login']);

        }else{

          if(res.code == 401){

            this.modal.modalGeneral("Crear cuenta", "Intente más tarde por favor.", "warning");

          }else{
            this.modal.modalGeneral("Crear cuenta", (res.message==undefined)?  res.message : "Ha habido un error con la creación de la cuenta. Por favor intente más tarde.", "error");
          }

        }
        
        
      }, error => {
        
        this.modal.modalGeneral("Crear cuenta", (error.error.message==undefined)? error.message : error.error.message, "error");
      }
        
      );

    }else{
      this.modal.modalGeneral("Crear cuenta", "La información ingreasda no es válida", "error");
    }

  }


  cambioEmail(email:any){
    this.datosCuenta.controls["usuario"].setValue(email.target.value.split('@')[0]);
  }

}
