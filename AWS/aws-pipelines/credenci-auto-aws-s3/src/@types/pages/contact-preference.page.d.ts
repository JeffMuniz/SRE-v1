
declare namespace ContactPreferencePage {
  namespace Form {

    type PreferredMedium = 'phone' | 'email';

    interface FormValues {
      preferredMedium: PreferredMedium;
    }
  }
}
