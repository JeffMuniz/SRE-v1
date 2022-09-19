
declare namespace AppDataStore {
  interface State {
    browsing: {
      history: Array<{
        action: ExternalModules.History.Action;
        location: ExternalModules.History.Location;
      }>;
    };
  }
}
