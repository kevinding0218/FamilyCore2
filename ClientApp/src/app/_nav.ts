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
        icon: 'icon-star'
      },
      {
        name: 'Vegetables',
        url: '/meal/vegetableList/vegetable',
        icon: 'icon-list'
      },
      {
        name: 'Meats',
        url: '/meal/meatList/meat',
        icon: 'icon-list'
      },
      {
        name: 'Seafood',
        url: '/meal/seafoodList/seafood',
        icon: 'icon-list'
      },
      {
        name: 'Ingredient',
        url: '/meal/ingredientList/ingredient',
        icon: 'icon-list'
      },
      {
        name: 'Sauce',
        url: '/meal/sauceList/sauce',
        icon: 'icon-list'
      },
      {
        name: 'Staple Food',
        url: '/meal/staplefoodList',
        icon: 'icon-list'
      }
    ]
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
