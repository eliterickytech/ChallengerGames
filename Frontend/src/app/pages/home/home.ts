import { Component, OnInit } from '@angular/core';
import { WorldCupService } from '../../service/world-cup/world-cup.service';
import { Game } from '../../models/games.model';
import Masonry from 'masonry-layout';
import { WorldCupResult } from '../../models/world-cup-result.model';
import { Router } from '@angular/router';

@Component({
  selector: 'home',
  templateUrl: './home.html',
  styleUrls: ['./home.css']
})

export class HomePage implements OnInit {
  game = {} as Game
	games: Game[]; 
  gamesGroupByYear: { [ano: number]: Game[] } = {};
  selectedGames: Game[] = []; 
  showResults: boolean = false;
  isLoading: boolean = false; 
  worldCupResults: WorldCupResult[];
  showSelectPanel: boolean = true;
  showResultsPanel: boolean = false;
  isButtonDisabled: boolean = true;

  constructor(private worldCupService: WorldCupService, private router: Router){

  }
  ngOnInit(){
    new Masonry('[data-masonry]');

    this.getAllGames();
  }
  
  getAllGames(){
    this.worldCupService.getAllGames().subscribe((games: Game[]) => {
      this.games = games;
      this.groupGamesByYear();
    });
  }

  groupGamesByYear() {
    this.games.forEach(game => {
      if (!this.gamesGroupByYear[game.ano]) {
        this.gamesGroupByYear[game.ano] = [];
      }
      this.gamesGroupByYear[game.ano].push(game);
    });
  }

  getKeys(obj: any): string[] {
    return Object.keys(obj);
  }

  onCheckboxChange(game: Game) {
    if (game.selected) {
      this.selectedGames = this.selectedGames.filter(selectedGame => selectedGame !== game);
      game.selected = false;
    } else if (this.selectedGames.length < 8) {
      this.selectedGames.push(game);
      game.selected = true;
    }
    this.checkSelectedGamesCount()
    console.log("Select Count: ", this.checkSelectedGamesCount())
  }

  generateWorldCup() {
    const selectedGameIds = this.selectedGames.map(game => game.id); 
    this.isLoading = true; 
    this.worldCupService.generateWorldCup(selectedGameIds).subscribe((results: WorldCupResult[]) => {
      const winners = results.filter(result => result.won);
      const nonWinners = results.filter(result => !result.won);

      this.worldCupResults = [...winners, ...nonWinners];
      this.worldCupResults = results;
    },
    error => {
      console.error('Error generate world cup:', error);
    }).add(() => {
      this.isLoading = false; 
      this.showSelectPanel = false;
      this.showResultsPanel = true;
    });
  }  

  goBack() {
    window.location.reload();
  }

  checkSelectedGamesCount() {
    this.isButtonDisabled = this.selectedGames.length !== 8;
    console.log("Button: ", this.isButtonDisabled)
  }

}


