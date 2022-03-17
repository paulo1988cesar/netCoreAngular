import { Router } from '@angular/router';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { Evento } from '@app/models/Evento';
import { EventoService } from '../../../services/evento.service';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-evento-lista',
  templateUrl: './evento-lista.component.html',
  styleUrls: ['./evento-lista.component.scss']
})
export class EventoListaComponent implements OnInit {
  modalRef?: BsModalRef;
  public eventos: Evento[] = [];
  public eventosFiltrados: Evento[] = [];
  mostrarImagem: boolean = true;
  private _txtEvento: string = '';

  public get txtEvento() {
    return this._txtEvento;
  }

  public set txtEvento(value : string) {
    this._txtEvento = value;
    this.eventosFiltrados = this.txtEvento ? this.FiltrarEventos(this.txtEvento) : this.eventos;
  }
  constructor(private eventoService: EventoService, private modalService: BsModalService,
              private toastrService: ToastrService, private spinner: NgxSpinnerService,
              private router: Router) {}

  ngOnInit(): void {
    this.spinner.show();
    this.getEventos();
  }

  public getEventos(): void {
    this.eventoService.getEventos().subscribe(
      (eventos: Evento[]) => {
        this.eventos = eventos;
        this.eventosFiltrados = eventos;
      },
      (error) => {
        this.spinner.hide()
        this.toastrService.error('Erro ao carregar os eventos.', 'Erro')
      },
      () => this.spinner.hide()
    );
  }

  public FiltrarEventos(filtrarPor: string): Evento[] {
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.eventos.filter(
      (evento : any) => evento.tema.toLocaleLowerCase().indexOf(filtrarPor) !== -1 ||
      evento.local.toLocaleLowerCase().indexOf(filtrarPor) !== -1
    )
  }

  public detalheEvento(id: number): void {
    this.router.navigate([`eventos/detalhe/${id}`])
  }

  public MostrarImagem(): void {
    this.mostrarImagem = !this.mostrarImagem;
  }

  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }

  confirm(): void {
    this.toastrService.success('O Evento foi excluído com sucesso.')
    this.modalRef?.hide();
  }

  decline(): void {
    this.modalRef?.hide();
  }
}