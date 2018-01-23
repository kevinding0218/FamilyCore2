export const navigation = [
  {
    name: 'Dashboard',
    url: '/dashboard',
    icon: 'icon-speedometer',
    badge: {
      variant: 'info',
      text: 'NEW'
    }
  },
  {
    name: 'Meal',
    url: '/meal',
    icon: 'icon-fire',
    children: [
      {
        name: 'Entrees',
        url: '/meal/entreeSummary',
        icon: 'fa fa-eercast'
      },
      {
        name: 'Vegetables',
        url: '/meal/vegetableList/vegetable',
        icon: 'fa fa-snowflake-o'
      },
      {
        name: 'Meats',
        url: '/meal/meatList/meat',
        icon: 'fa fa-superpowers'
      },
      {
        name: 'Seafood',
        url: '/meal/seafoodList/seafood',
        icon: 'fa fa-telegram'
      },
      {
        name: 'Ingredient',
        url: '/meal/ingredientList/ingredient',
        icon: 'fa fa-bandcamp'
      },
      {
        name: 'Sauce',
        url: '/meal/sauceList/sauce',
        icon: 'fa fa-free-code-camp'
      },
      {
        name: 'Staple Food',
        url: '/meal/staplefoodList',
        icon: 'icon-star'
      }
    ]
  },
  {
    name: 'Order',
    url: '/order',
    icon: 'icon-calculator',
    badge: {
      variant: 'success',
      text: '12'
    }
  },
  {
    name: 'Pages',
    url: '/pages',
    icon: 'icon-star',
    children: [
      {
        name: 'Login',
        url: '/pages/login',
        icon: 'icon-star'
      },
      {
        name: 'Register',
        url: '/pages/register',
        icon: 'icon-star'
      },
      {
        name: 'Error 404',
        url: '/pages/404',
        icon: 'icon-star'
      },
      {
        name: 'Error 500',
        url: '/pages/500',
        icon: 'icon-star'
      }
    ]
  }
];
