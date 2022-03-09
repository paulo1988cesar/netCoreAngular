import { EventosComponent } from './componentes/eventos/eventos.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './componentes/dashboard/dashboard.component';
import { ContatoComponent } from './componentes/contato/contato.component';
import { PerfilComponent } from './componentes/perfil/perfil.component';
import { PaletrantesComponent } from './componentes/paletrantes/paletrantes.component';

const routes: Routes = [
  { path: 'eventos', component: EventosComponent},
  { path: 'dashboard', component: DashboardComponent},
  { path: 'contato', component: ContatoComponent},
  { path: 'perfil', component: PerfilComponent},
  { path: 'palestrantes', component: PaletrantesComponent},
  { path: '', redirectTo: 'dashboard', pathMatch: 'full'},
  { path: '**', redirectTo: 'dashboard', pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
