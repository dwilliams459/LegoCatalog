import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { PartComponent } from './part/part.component';
import { PartlistComponent } from './partlist/partlist.component';


const routes: Routes = [
  { path: 'home', component: HomeComponent },
  { path: 'part', component: PartComponent },
  { path: 'partlist', component: PartlistComponent },
  { path: '', redirectTo: 'partlist', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
