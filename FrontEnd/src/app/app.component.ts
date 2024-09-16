import { Component, ElementRef } from '@angular/core';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css',
    '../../node_modules/bootstrap/dist/css/bootstrap.min.css'
  ]
})
export class AppComponent {
  constructor(private elementRef:ElementRef) { }
  title = 'routing-demo';
  ngOnInit(): void {
    // Example of the dom in angular
    // this.elementRef.nativeElement.querySelector('#checkLoad')
    //                             .addEventListener('load', () => {
    //                               console.log('In Onload');
    //                               location.href = './welcome';
    //                             });
  }
}
