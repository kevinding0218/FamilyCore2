import { AppNgxBootstrapModule } from './../../ngxModule/app-ngx-bootstrap.module';
import { PaginationComponent } from './../../components/table-pagination/pagination.component';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MealRoutingModule } from './meal-routing.module';

// import entree common
import {
  EntreeDetailCartTemplateComponent,
  EntreeDetailCommonSectionComponent,
  EntreeFormCommonComponent,
  EntreeListCommonComponent,
  EntreeByStyleWidgetComponent,
  EntreeByCatagoryWidgetComponent,
  EntreeDetailFormComponent,
  EntreeDetailListComponent
} from './common';

const ENTREE_COMMON_COMPONENTS = [
  EntreeDetailCartTemplateComponent,
  EntreeDetailCommonSectionComponent,
  EntreeFormCommonComponent,
  EntreeListCommonComponent,
  EntreeByStyleWidgetComponent,
  EntreeByCatagoryWidgetComponent,
  EntreeDetailFormComponent,
  EntreeDetailListComponent
]

// import entree
import {
  EntreeSummaryBoardComponent,
  EntreeListByStyleComponent
} from './entree';

const ENTREE_UI_COMPONENTS = [
  EntreeSummaryBoardComponent,
  EntreeListByStyleComponent
]

// import staplefood
import {
  StaplefoodFormComponent,
  StaplefoodListComponent
} from './staplefood';

const STAPLEFOOD_COMPONENTS = [
  StaplefoodFormComponent,
  StaplefoodListComponent
]

// import vegetable
import {
  VegetableListComponent,
  VegetableFormComponent
} from './vegetable';

const VEGETABLE_COMPONENTS = [
  VegetableListComponent,
  VegetableFormComponent
]

// import meat
import {
  MeatFormComponent,
  MeatListComponent
} from './meat';

const MEAT_COMPONENTS = [
  MeatFormComponent,
  MeatListComponent
]

// import sauce
import {
  SauceFormComponent,
  SauceListComponent
} from './sauce';

const SAUCE_COMPONENTS = [
  SauceFormComponent,
  SauceListComponent
]

// import ingredient
import {
  IngredientListComponent,
  IngredientFormComponent
} from './ingredient';

const INGREDIENT_COMPONENTS = [
  IngredientListComponent,
  IngredientFormComponent
]

// import seafood
import {
  SeafoodListComponent,
  SeafoodFormComponent
} from './seafood';

const SEAFOOD_COMPONENTS = [
  SeafoodListComponent,
  SeafoodFormComponent
]

// import service
import {
  EntreeDetailService,
  StaplefoodService,
  EntreeService,
  EntreeHelperService,
  CurrentOrderService,
  EntreePhotoUploadService
} from './../../services';

const SERVICE_COMPONENTS = [
  EntreeDetailService,
  StaplefoodService,
  EntreeService,
  EntreeHelperService,
  CurrentOrderService,
  EntreePhotoUploadService
]


@NgModule({
  imports: [ FormsModule, CommonModule, MealRoutingModule, AppNgxBootstrapModule ],
  declarations: [
    ...ENTREE_COMMON_COMPONENTS,
    ...ENTREE_UI_COMPONENTS,
    ...VEGETABLE_COMPONENTS,
    ...MEAT_COMPONENTS,
    ...STAPLEFOOD_COMPONENTS,
    ...SAUCE_COMPONENTS,
    ...SEAFOOD_COMPONENTS,
    ...INGREDIENT_COMPONENTS,
  ],
  providers: [ 
    ...SERVICE_COMPONENTS   
  ]
})
export class MealModule { }
