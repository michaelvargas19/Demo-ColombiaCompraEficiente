import { MediaMatcher } from '@angular/cdk/layout';
import { ChangeDetectorRef, Component, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ModalComponent } from 'src/app/shared/modal/modal.component';
import { Credenciales } from 'src/app/shared/model/account/credenciales';
import { LoginResponse } from 'src/app/shared/model/account/query/login-response';
import { ResponseBase } from 'src/app/shared/model/resquest-base';
import { CuentaService } from 'src/app/shared/services/cuenta.service';
import { environment } from 'src/environments/environment.prod';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  @ViewChild(ModalComponent,{ static: false }) modal: ModalComponent;

  loginForm = new FormGroup({
    usuario: new FormControl('', Validators.required),
    contrasena: new FormControl('', Validators.required)
  })

  private _mobileQueryListener: () => void;
  mobileQuery: MediaQueryList;



  constructor(private router: Router, private CuentaService :CuentaService,
              changeDetectorRef: ChangeDetectorRef, media: MediaMatcher)
              {
                this.modal= new ModalComponent();
                this.mobileQuery = media.matchMedia('(max-width: 1200px)');
                this._mobileQueryListener = () => changeDetectorRef.detectChanges();
                this.mobileQuery.addListener(this._mobileQueryListener); 
              }

  ngOnInit(): void {
  }


  iniciarSesion() {

    this.modal.mostraCargando();
    
    let credentials = new Credenciales();

    credentials.idAplicacion = environment.ID_APPLICACION;
    credentials.usuario = this.loginForm.controls["usuario"].value;
    credentials.contrasena = this.loginForm.controls["contrasena"].value;

    this.CuentaService.authenticate(credentials).subscribe((res: ResponseBase<LoginResponse>) => {
      
      if (res.code == 200) {

        if (res.data.autenticado ) {
          
          this.CuentaService.cargarUsuario(res.data.datosUsuario.usuario, res.data);
          this.router.navigate(['/inicio']);
          this.modal.modalGeneralTimer("Bienvenido", (res.data==undefined)?  res.message : res.data.mensaje, "success",1000);
        } else {
          
            this.modal.modalGeneral("Inicio Sesión", (res.data==undefined)?  res.message : res.data.mensaje, "warning");
        }

      }else{
        this.modal.modalGeneral("Inicio Sesión", (res.data==undefined)?  res.message : res.data.mensaje, "warning");
      } 
    }, error => {
      this.modal.modalGeneral("Inicio Sesión", error.message, "error");
    }
      
    );
    
  }


}
