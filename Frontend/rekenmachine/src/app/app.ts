import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Rekenmachine } from "./components/rekenmachine/rekenmachine";

@Component({
  selector: 'app-root',
  imports: [Rekenmachine],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App {
  protected readonly title = signal('rekenmachine');
}
