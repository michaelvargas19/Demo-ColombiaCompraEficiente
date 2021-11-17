import { MediaMatcher } from '@angular/cdk/layout';
import { ChangeDetectorRef, Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { ModalComponent } from '../shared/modal/modal.component';
import { CuentaService } from '../shared/services/cuenta.service';

@Component({
  selector: 'app-navegation',
  templateUrl: './navegation.component.html',
  styleUrls: ['./navegation.component.css']
})
export class NavegationComponent implements OnInit {

  private _mobileQueryListener: () => void;

  @ViewChild(ModalComponent,{ static: false }) modal: ModalComponent;
  //@ViewChild(MenuItemsComponent,{ static: false }) menuItems: MenuItemsComponent;
  //@Output() emitterMenu = new EventEmitter<Menu>();

  // variables Globales
  usuario: string;
  mobileQuery: MediaQueryList;


  constructor(changeDetectorRef: ChangeDetectorRef, media: MediaMatcher,
    private router: Router,
    private CuentaService :CuentaService)
{

this.mobileQuery = media.matchMedia('(max-width: 600px)');
this._mobileQueryListener = () => changeDetectorRef.detectChanges();
this.mobileQuery.addListener(this._mobileQueryListener); 
}

  ngOnInit(): void {
    this.mobileQuery.removeListener(this._mobileQueryListener);
    //this.AccountService.logout();
  }



  datosUsuario(usuario: any) {
    this.usuario = usuario;
  }

  volverAlInicio(){
    this.router.navigate(['/productos']);
  }

  iniciarSesion(){
    this.router.navigate(['/login']);
  }

  cerrarSesion(){
    return this.CuentaService.logout();
  }

  registrarse(){
    this.router.navigate(['/registro']);
  }

  estaAutenticado(){
    return this.CuentaService.isAuthenticated();
  }
 
  verProductos(){
    this.router.navigate(['/productos']);
  }
}
