import { Component, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Controller } from '../../services/controller';
import { MatButtonModule } from '@angular/material/button';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatInputModule } from '@angular/material/input';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-rekenmachine',
  standalone: true,
  imports: [
    CommonModule,
    MatButtonModule,
    MatGridListModule,
    MatInputModule,
    FormsModule
  ],
  templateUrl: './rekenmachine.html',
  styleUrl: './rekenmachine.scss'
})
export class Rekenmachine {
  controller = inject(Controller);

  equation = signal<string>("");

  //Clear the equation or result
  clearEquation() {
    this.equation.set("");
  }

  //Add to the equation
  addToEquation(value: string) {
    this.equation.update(old => old += value);
  }

  //Send the equation to the back-end and receive the results of the equation
  calculate() {
    this.controller.performCalculation({ "equation": this.equation.toString() })
      .subscribe({
        next: (response) => {
          this.equation.set(response.equation);
        },
        error: (err) => console.log(err)
      });
  }
}
