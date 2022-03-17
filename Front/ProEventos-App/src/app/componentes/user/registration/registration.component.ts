import { ValidatorField } from './../../../helpers/ValidatorField';
import { AbstractControlOptions, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss']
})
export class RegistrationComponent implements OnInit {

  form!: FormGroup;

  constructor(private fb: FormBuilder) { }

  ngOnInit(): void {
    this.Validation();
  }

  private Validation(): void {

    const formOptions: AbstractControlOptions = {
      validators: ValidatorField.MustMath('senha', 'confirmasenha')
    };

    this.form = this.fb.group({
      primeironome: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(20)]],
      segundonome: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(20)]],
      email: ['', [Validators.required, Validators.email]],
      usuario: ['', [Validators.required, Validators.minLength(8)]],
      senha: ['', Validators.required, Validators.minLength(8), Validators.maxLength(10)],
      confirmasenha: ['', [Validators.required, Validators.minLength(8), Validators.maxLength(10)]]
    }, formOptions);
  }

  public get f(): any {
    return this.form.controls;
  }
}
