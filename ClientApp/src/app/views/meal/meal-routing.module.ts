import { SauceListComponent } from './sauce/list/sauce-list.component';
import { SauceFormComponent } from './sauce/form/sauce-form.component';
import { StaplefoodFormComponent } from './staplefood/staplefood-form/staplefood-form.component';
import { MeatFormComponent } from './meat/meat-form/meat-form.component';
import { MeatListComponent } from './meat/meat-list/meat-list.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { VegetableListComponent } from './vegetable/vegetable-list/vegetable-list.component';
import { VegetableFormComponent } from './vegetable/vegetable-form/vegetable-form.component';
import { StaplefoodListComponent } from './staplefood/staplefood-list/staplefood-list.component';

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
        path: 'vegetableList/:type',
        component: VegetableListComponent,
        data: {
          title: 'Vegetable List'
        }
      },
      {
        path: 'vegetableForm/:type/new',
        component: VegetableFormComponent,
        data: {
          title: 'Create New Vegetable'
        }
      },
      {
        path: 'vegetableForm/:type/:id',
        component: VegetableFormComponent,
        data: {
          title: 'Update Vegetable'
        }
      },
      {
        path: 'meatList/:type',
        component: MeatListComponent,
        data: {
          title: 'Meat List'
        }
      },
      {
        path: 'meatForm/:type/new',
        component: MeatFormComponent,
        data: {
          title: 'Create New Meat'
        }
      },
      {
        path: 'meatForm/:type/:id',
        component: MeatFormComponent,
        data: {
          title: 'Update Meat'
        }
      },
      {
        path: 'staplefoodList',
        component: StaplefoodListComponent,
        data: {
          title: 'Meat List'
        }
      },
      {
        path: 'staplefoodForm/new',
        component: StaplefoodFormComponent,
        data: {
          title: 'Create New Staple Food'
        }
      },
      {
        path: 'staplefoodForm/:id',
        component: StaplefoodFormComponent,
        data: {
          title: 'Update Staple Food'
        }
      },
      {
        path: 'sauceList/:type',
        component: SauceListComponent,
        data: {
          title: 'Sauce List'
        }
      },
      {
        path: 'sauceForm/:type/new',
        component: SauceFormComponent,
        data: {
          title: 'Create New Sauce'
        }
      },
      {
        path: 'SauceForm/:type/:id',
        component: SauceFormComponent,
        data: {
          title: 'Update Sauce'
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
