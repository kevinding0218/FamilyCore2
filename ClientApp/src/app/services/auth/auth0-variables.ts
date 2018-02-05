interface AuthConfig {
    clientID: string;
    domain: string;
    callbackURL: string;
  }
  
  export const AUTH_CONFIG: AuthConfig = {
    clientID: 'l21dXakumBhIcZrzLzGu8QaSzb7sDrAj',
    domain: 'familycore.auth0.com',
    callbackURL: 'http://localhost:4200/pages/callback'
  };
  