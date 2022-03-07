import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.scss'],
})
export class EventosComponent implements OnInit {
  public eventos: any = [];
  public eventosFiltrados: any= [];
  mostrarImagem: boolean = true;
  private _txtEvento: string = '';

  public get txtEvento() {
    return this._txtEvento;
  }

  public set txtEvento(value : string) {
    this._txtEvento = value;
    this.eventosFiltrados = this.txtEvento ? this.FiltrarEventos(this.txtEvento) : this.eventos;
  }
  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.getEventos();
  }

  public getEventos(): void {
    this.http.get('https://localhost:5001/api/Eventos/Getall').subscribe(
      (response) => {
        this.eventos = response;
        this.eventosFiltrados = response;
      },
      (error) => console.log(error)
    );
  }

  public FiltrarEventos(filtrarPor: string): any {
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.eventos.filter(
      (evento : any) => evento.tema.toLocaleLowerCase().indexOf(filtrarPor) !== -1 ||
      evento.local.toLocaleLowerCase().indexOf(filtrarPor) !== -1
    )
  }

  public MostrarImagem(): void {
    this.mostrarImagem = !this.mostrarImagem;
  }
}
