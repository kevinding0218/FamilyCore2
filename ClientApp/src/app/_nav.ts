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
        name: 'Vegetables',
        url: '/meal/vegetableList',
        icon: 'icon-list'
      },
      {
        name: 'Meats',
        url: '/meal/meatList',
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
