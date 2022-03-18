import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Evento } from '@app/models/Evento';
import { EventoService } from '@app/services/evento.service';
import { BsLocaleService } from 'ngx-bootstrap/datepicker';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-evento-detalhe',
  templateUrl: './evento-detalhe.component.html',
  styleUrls: ['./evento-detalhe.component.scss'],
  providers: [DatePipe]
})
export class EventoDetalheComponent implements OnInit {
  public form!: FormGroup;
  evento = {} as Evento;
  estadoSalvar = 'post';

  constructor(private fb: FormBuilder, private localeService: BsLocaleService,
    private eventoService: EventoService, private router: ActivatedRoute,
    private spinner: NgxSpinnerService, private toasteService: ToastrService,
    private rota: Router ) {
    this.localeService.use('pt-br');
  }

  ngOnInit(): void {
    this.carregarEvento();
    this.validation();
  }

  public carregarEvento(): void {

    const eventoId = this.router.snapshot.paramMap.get('id');

    if (eventoId !== null)
    {
      this.spinner.show();

      this.estadoSalvar = 'put';

      this.eventoService.getEventoById(Number(eventoId)).subscribe(
        (evento: Evento) => {
          this.evento = { ...evento };
          this.form.patchValue(this.evento);
        },
        (error) => {
          this.toasteService.error('Erro ao buscar as informações de usuário', 'Erro!')
        }
      ).add(() => this.spinner.hide());
    }
  }

  public salvarAlteracao(): void {
    this.spinner.show();
    if (this.form.valid) {

      this.evento = (this.estadoSalvar == 'post')
                  ? {...this.form.value}
                  : {id: this.router.snapshot.paramMap.get('id'), ...this.form.value};

      this.eventoService.salvarEvento(this.evento).subscribe(
        () => {
          this.toasteService.success('Evento salvo com sucesso.', 'Sucesso');
          this.rota.navigate(['eventos/lista'])
        },
        (erro: any) => {
          this.toasteService.error('Erro ao salvar o evento.', 'Erro!')
        }
      ).add(() => this.spinner.hide());
    }
  }

  public validation(): void {
    this.form = this.fb.group({
      tema: ['', [ Validators.required, Validators.minLength(4), Validators.maxLength(100)]],
      local: ['', [Validators.required, Validators.min(3)]],
      dataEvento: ['', Validators.required],
      qtdPessoas: ['', [Validators.required, Validators.max(120000)]],
      telefone: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      imageUrl: ['', Validators.required]
    });
  }

  public resertForm(): void {
    this.form.reset();
  }

  public get bsConfig(): any {
    return {
      isAnimated: true,
      adaptivePosition: true,
      dateInputFormat: 'DD/MM/YYY hh:mm a',
      containerClass: 'theme-default',
      showWeekNumbers: false,
    };
  }
  public get f(): any {
    return this.form.controls;
  }

  public cssValidator(campoForm: FormControl): any {
    return { 'is-invalid': campoForm.errors && campoForm.touched };
  }
}
