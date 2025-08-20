import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideClientHydration } from '@angular/platform-browser';
import { provideAuth0, AuthHttpInterceptor } from '@auth0/auth0-angular';
import { HTTP_INTERCEPTORS, provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { environment } from '../environments/environment';

export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
    provideClientHydration(),
  // Attach the Auth0 HTTP interceptor via DI for compatibility
  provideHttpClient(withInterceptorsFromDi()),
  { provide: HTTP_INTERCEPTORS, useClass: AuthHttpInterceptor, multi: true },
    // Initialize Auth0 for a standalone app
    provideAuth0({
      domain: environment.auth0.domain,
      clientId: environment.auth0.clientId,
  // Skip redirect callback checks during SSR to avoid referencing window/location
  skipRedirectCallback: typeof window === 'undefined',
      authorizationParams: {
        audience: environment.auth0.audience,
        // Guard against SSR where window is undefined
        redirect_uri: typeof window !== 'undefined' ? window.location.origin : undefined as any
      },
      httpInterceptor: {
        allowedList: [
          {
            uri: 'https://api.dispofinder/*',
            tokenOptions: {
              authorizationParams: { audience: environment.auth0.audience }
            }
          }
        ]
      }
    }),
  ]
};

