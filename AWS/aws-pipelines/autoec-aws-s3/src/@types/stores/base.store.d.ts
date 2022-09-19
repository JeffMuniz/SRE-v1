
type StorageController = ExternalModules.MobxPersistStore.StorageController;

declare namespace BaseStore {

  interface Options {
    persistence?: {
      storage: StorageController;
      key: string;
      expiresIn?: number;
    };
  }

}
