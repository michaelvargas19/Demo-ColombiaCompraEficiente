import { MediaMatcher } from '@angular/cdk/layout';
import { ChangeDetectorRef, Component } from '@angular/core';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Portal Web';

  private _mobileQueryListener: () => void;
  mobileQuery: MediaQueryList;

  author = 'Michael Vargas';
  year = (new Date()).getFullYear();
  version = '1.0.0';

  constructor(changeDetectorRef: ChangeDetectorRef, media: MediaMatcher)
  {
    this.mobileQuery = media.matchMedia('(max-width: 600px)');
    this._mobileQueryListener = () => changeDetectorRef.detectChanges();
    this.mobileQuery.addListener(this._mobileQueryListener); 
  }


}
