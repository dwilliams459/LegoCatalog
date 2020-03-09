import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { TimelineComponent } from './timeline/timeline.component';
import { PartComponent } from './part/part.component';
import { PartlistComponent } from './partlist/partlist.component';
import { PartgridComponent } from './partgrid/partgrid.component';
//import { OktaCallbackComponent, OktaAuthGuard } from '@okta/okta-angular';

const routes: Routes = [
  {
    path: '',
    component: HomeComponent
  },
  // {
  //   path: 'timeline',
  //   component: TimelineComponent
  // },
  {
    path: 'part',
    component: PartComponent
  },
  {
    path: 'partlist',
    component: PartlistComponent
  },
  { 
    path: 'partgrid',
    component: PartgridComponent
  }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
