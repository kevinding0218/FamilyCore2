import { MeatFormComponent } from './meat/meat-form/meat-form.component';
import { MeatListComponent } from './meat/meat-list/meat-list.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { VegetableListComponent } from './vegetable/vegetable-list/vegetable-list.component';
import { VegetableFormComponent } from './vegetable/vegetable-form/vegetable-form.component';

const routes: Routes = [
  {
    path: '',
    data: {
      title: 'Meal'
    },
    children: [
      {
        path: '',
        component: VegetableListComponent,
        data: {
          title: 'Vegetables'
        }
      },
      {
        path: 'vegetableList',
        component: VegetableListComponent,
        data: {
          title: 'Vegetable List'
        }
      },
      {
        path: 'vegetableForm/new',
        component: VegetableFormComponent,
        data: {
          title: 'Create New Vegetable'
        }
      },
      {
        path: 'vegetableForm/:id',
        component: VegetableFormComponent,
        data: {
          title: 'Update Vegetable'
        }
      },
      {
        path: 'meatList',
        component: MeatListComponent,
        data: {
          title: 'Meat List'
        }
      },
      {
        path: 'meatForm/new',
        component: MeatFormComponent,
        data: {
          title: 'Create New Meat'
        }
      },
      {
        path: 'meatForm/:id',
        component: MeatFormComponent,
        data: {
          title: 'Update Vegetable'
        }
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MealRoutingModule {}
