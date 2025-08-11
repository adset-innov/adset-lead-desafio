import { provideHttpClient } from '@angular/common/http';
import { provideRouter } from '@angular/router';
import { importProvidersFrom } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';

export const appConfig = {
  providers: [
    provideHttpClient(),
    provideRouter([]),
    importProvidersFrom(ReactiveFormsModule),
  ],
};
