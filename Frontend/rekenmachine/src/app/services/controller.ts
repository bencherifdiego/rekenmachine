import { Injectable, signal } from '@angular/core';
import { Calculation } from '../interfaces/calculation.interface';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class Controller {
  constructor(
    private http: HttpClient
  ) {}

  //Send post request to back-end
  performCalculation(param: Calculation): Observable<Calculation> {
    return this.http.post<Calculation>('http://localhost:5123/api/calculate', param);
  }
}
