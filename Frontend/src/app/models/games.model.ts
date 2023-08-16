import { DecimalPipe } from "@angular/common";

export interface Game {
    id: string;
    titulo: string;
    nota: DecimalPipe ;
    ano: number;
    urlImagem: string;
    console: string;
    selected: boolean;
}