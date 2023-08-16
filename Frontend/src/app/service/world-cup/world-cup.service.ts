import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { retry, catchError } from 'rxjs/operators';
import { Game } from '../../models/games.model';
import { environment } from '../../../environments/environment';
import { WorldCupResult } from '../../models/world-cup-result.model';

@Injectable({
  providedIn: 'root'
})

export class WorldCupService {


  constructor(private httpClient: HttpClient) { }

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  }

  getAllGames(): Observable<Game[]> {
    const endpoint = `${environment.apiBaseUrl}/GamesAll`;

    return this.httpClient.get<Game[]>(endpoint)
      .pipe(
        retry(2),
        catchError(this.handleError))
  }

  generateWorldCup(selectedGameIds: string[]): Observable<WorldCupResult[]> {
    const endpoint = `${environment.apiBaseUrl}/GenerateWorldCup`; 

    const gameInputs = selectedGameIds.map(id => ({ id }));

    return this.httpClient.post<WorldCupResult[]>(endpoint, gameInputs)
      .pipe(
        retry(2),
        catchError(this.handleError))
  }

  handleError(error: HttpErrorResponse) {
    let errorMessage = '';
    if (error.error instanceof ErrorEvent) {

      errorMessage = error.error.message;
    } else {

      errorMessage = `code ${error.status}, ` + `message: ${error.message}`;
    }
    console.log(errorMessage);
    return throwError(errorMessage);
  };
}
  