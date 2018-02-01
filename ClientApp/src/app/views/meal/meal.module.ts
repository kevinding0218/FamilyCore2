import { EntreePhotoUploadService } from './../../services/upload/entree-photo-upload.service';
import { CurrentOrderService } from './../../services/order/current-order.service';
import { EntreeByStyleWidgetComponent } from './common/entree/widget/entree-by-style-widget.component';
import { EntreeByCatagoryWidgetComponent } from './common/entree/widget/entree-by-catagory-widget.component';
import { EntreeSummaryBoardComponent } from './entree/form/entree-summary-board.component';
import { EntreeDetailCartTemplateComponent } from './common/entree/form/entreeDetailSection/entree-detail-cart-template/entree-detail-cart-template.component';
import { EntreeDetailCommonSectionComponent } from './common/entree/form/entreeDetailSection/entree-detail-common-section.component';
import { EntreeHelperService } from './../../services/meal/entree-helper.service';
import { EntreeFormCommonComponent } from './common/entree/form/entree-form-common.component';
import { EntreeListByStyleComponent } from './entree/list/entree-list-by-style.component';
import { EntreeService } from './../../services/meal/entree.service';
import { EntreeListCommonComponent } from './common/entree/list/entree-list-common.component';
import { IngredientListComponent } from './ingredient/list/ingredient-list.component';
import { IngredientFormComponent } from './ingredient/form/ingredient-form.component';
import { SeafoodFormComponent } from './seafood/form/seafood-form.component';
import { SeafoodListComponent } from './seafood/list/seafood-list.component';
import { EntreeDetailListComponent } from './common/entree-detail/list/entree-detail.list.component';
import { EntreeDetailService } from './../../services/meal/entree-detail.service';
import { SauceFormComponent } from './sauce/form/sauce-form.component';
import { SauceListComponent } from './sauce/list/sauce-list.component';
import { EntreeDetailFormComponent } from './common/entree-detail/form/entree-detail-form.component';
import { StaplefoodService } from './../../services/meal/staplefood.service';
import { MeatFormComponent } from './meat/meat-form/meat-form.component';
import { AppNgxBootstrapModule } from './../../ngxModule/app-ngx-bootstrap.module';
import { PaginationComponent } from './../../components/table-pagination/pagination.component';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MealRoutingModule } from './meal-routing.module';
import { VegetableListComponent } from './vegetable/vegetable-list/vegetable-list.component';
import { VegetableFormComponent } from './vegetable/vegetable-form/vegetable-form.component';
import { MeatListComponent } from './meat/meat-list/meat-list.component';
import { StaplefoodFormComponent } from './staplefood/staplefood-form/staplefood-form.component';
import { StaplefoodListComponent } from './staplefood/staplefood-list/staplefood-list.component';

@NgModule({
  imports: [ FormsModule, CommonModule, MealRoutingModule, AppNgxBootstrapModule ],
  declarations: [
    EntreeDetailFormComponent,
    EntreeDetailListComponent,
    VegetableListComponent,
    VegetableFormComponent,
    PaginationComponent,
    MeatListComponent,
    MeatFormComponent,
    StaplefoodFormComponent,
    StaplefoodListComponent,
    SauceFormComponent,
    SauceListComponent,
    SeafoodFormComponent,
    SeafoodListComponent,
    IngredientFormComponent,
    IngredientListComponent,
    EntreeByCatagoryWidgetComponent,
    EntreeByStyleWidgetComponent,
    EntreeSummaryBoardComponent,
    EntreeListCommonComponent,
    EntreeListByStyleComponent,
    EntreeDetailCartTemplateComponent,
    EntreeDetailCommonSectionComponent,
    EntreeFormCommonComponent
  ],
  providers: [ 
    StaplefoodService, 
    EntreeDetailService, 
    EntreeService, 
    EntreeHelperService, 
    CurrentOrderService, 
    EntreePhotoUploadService    
  ]
})
export class MealModule { }
