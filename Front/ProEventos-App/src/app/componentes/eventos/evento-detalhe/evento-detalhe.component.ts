import { environment } from './../../../../environments/environment';
import { Lote } from './../../../models/Lote';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { AbstractControl, FormArray, FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Evento } from '@app/models/Evento';
import { EventoService } from '@app/services/evento.service';
import { BsLocaleService } from 'ngx-bootstrap/datepicker';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';
import { DatePipe } from '@angular/common';
import { LoteService } from '@app/services/lote.service';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-evento-detalhe',
  templateUrl: './evento-detalhe.component.html',
  styleUrls: ['./evento-detalhe.component.scss'],
  providers: [DatePipe],
})
export class EventoDetalheComponent implements OnInit {
  public form!: FormGroup;
  evento = {} as Evento;
  estadoSalvar = 'post';
  eventoId: number;
  modalRef?: BsModalRef;
  loteAtual= {id : 0, nome: '', index: 0};
  imagemURL = 'assets/upload.png';
  file: File;

  constructor(private fb: FormBuilder, private localeService: BsLocaleService, private eventoService: EventoService,
              private activatedRouter: ActivatedRoute, private spinner: NgxSpinnerService, private toasteService: ToastrService,
              private router: Router, private loteService: LoteService, private modalService: BsModalService) {
    this.localeService.use('pt-br');
  }

  ngOnInit(): void {
    this.carregarEvento();
    this.validation();
  }

  public modoEditar(): boolean {
    return this.estadoSalvar === 'put';
  }

  public carregarEvento(): void {

    if (this.activatedRouter.snapshot.paramMap.get('id') !== '')
      this.eventoId = Number(this.activatedRouter.snapshot.paramMap.get('id'));

    if (this.eventoId !== null && this.eventoId > 0) {
      this.spinner.show();

      this.estadoSalvar = 'put';

      this.eventoService
        .getEventoById(this.eventoId).subscribe(
          (evento: Evento) => {
            this.evento = { ...evento };
            this.form.patchValue(this.evento);
            if (this.evento.imageUrl !== '') {
              this.imagemURL = environment.apiUrl + 'resources/images/' + this.evento.imageUrl;
            }
            this.evento.lotes.forEach((lote) => {
              this.lotes.push(this.criarLote(lote));
            });
          },
          (error) => {
            this.toasteService.error(
              'Erro ao buscar as informações de usuário', 'Erro!'
            );
          }
        ).add(() => this.spinner.hide());
    } else { this.estadoSalvar = 'post'; }
  }

  public salvarEvento(): void {
    this.spinner.show();
    if (this.form.valid) {
      this.evento =
        this.estadoSalvar == 'post'
          ? { ...this.form.value }
          : {
              id: this.activatedRouter.snapshot.paramMap.get('id'),
              ...this.form.value,
            };

      this.eventoService
        .salvarEvento(this.evento)
        .subscribe(
          (eventoRetorno: Evento) => {
            this.toasteService.success('Evento salvo com sucesso.', 'Sucesso');
            this.router.navigate([`eventos/detalhe/${eventoRetorno.id}`]);
          },
          (erro: any) => {
            this.toasteService.error('Erro ao salvar o evento.', 'Erro!');
          }
        ).add(() => this.spinner.hide());
    }
  }

  public salvarLote(): void {
    if (this.form.controls.lotes.valid) {

      this.spinner.show();

      this.loteService
        .saveLote(this.eventoId, this.form.controls.lotes.value)
        .subscribe(
          () => {
            this.toasteService.success('Lotes salvos com sucesso', 'Sucesso!');
            this.router.navigate([`eventos/detalhe/${this.eventoId}`]);
          },
          (error) => {
            this.toasteService.error('Erro ao tentar salvar os lotes');
          }
        ).add(() => this.spinner.hide());
    }
  }

  public validation(): void {
    this.form = this.fb.group({
      tema: [
        '',
        [
          Validators.required,
          Validators.minLength(4),
          Validators.maxLength(100),
        ],
      ],
      local: ['', [Validators.required, Validators.min(3)]],
      dataEvento: ['', Validators.required],
      qtdPessoas: ['', [Validators.required, Validators.max(120000)]],
      telefone: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      imageUrl: [''],
      lotes: this.fb.array([]),
    });
  }

  public adicionarLote(): void {
    this.lotes.push(this.criarLote({ id: 0 } as Lote));
  }

  public resertForm(): void {
    this.form.reset();
  }

  public get bsConfig(): any {
    return {
      isAnimated: true,
      adaptivePosition: true,
      dateInputFormat: 'DD/MM/YYYY hh:mm a',
      containerClass: 'theme-default',
      showWeekNumbers: false,
    };
  }

  public get bsConfigLote(): any {
    return {
      isAnimated: true,
      adaptivePosition: true,
      dateInputFormat: 'DD/MM/YYYY',
      containerClass: 'theme-default',
      showWeekNumbers: false,
    };
  }

  public get f(): any {
    return this.form.controls;
  }

  public get lotes(): FormArray {
    return this.form.get('lotes') as FormArray;
  }

  public cssValidator(campoForm: FormControl | AbstractControl): any {
    return { 'is-invalid': campoForm.errors && campoForm.touched };
  }

  private criarLote(lote: Lote): FormGroup {
    return this.fb.group({
      id: [lote.id],
      nome: [lote.nome, Validators.required],
      preco: [lote.preco, Validators.required],
      quantidade: [lote.quantidade, Validators.required],
      dataInicio: [lote.dataInicio],
      dataFim: [lote.dataFim]
    });
  }

  public excluirLote(template: TemplateRef<any>, index: number): void {
    this.loteAtual.id = this.lotes.get(index + '.id').value;
    this.loteAtual.nome = this.lotes.get(index + '.nome').value;
    this.loteAtual.index = index;
    this.modalRef = this.modalService.show(template, { class: 'modal-sm' });
  }

  public confirmDeleteLote(): void {
    this.modalRef.hide();
    this.spinner.show();

    this.loteService.deleteLote(this.eventoId, this.loteAtual.id).subscribe(
      () => {
        this.toasteService.success('Lote excluído com sucesso', 'Sucesso!');
        this.lotes.removeAt(this.loteAtual.index);
      },
      () => {
        this.toasteService.error('O Lote foi excluído com sucesso', 'Erro!')
      }
    ).add(() => this.spinner.hide());
  }

  public declineDeleteLote(): void {
    this.modalRef.hide();
  }

  public retornaTituloLote(nome: string) : string {
    return nome === null || nome === '' ? 'Nome do Lote' : nome;
  }

  public OnFileChange(event: any) : void {
    const reader = new FileReader();

    reader.onload = (e: any) => this.imagemURL = e.target.result;

    this.file = event.target.files;
    reader.readAsDataURL(this.file[0]);

    this.uploadImage();
  }

  public uploadImage() : void {
    this.spinner.show();
    this.eventoService.postUpload(this.eventoId, this.file).subscribe(
      () => {
        this.carregarEvento();
        this.toasteService.success('A imagem foi guardada com sucesso', 'Sucesso!');
      },
      (error) => {
        this.toasteService.error('Erro ao guardar a imagem', 'Erro!');
      }
    ).add(() => this.spinner.hide());
  }
}
