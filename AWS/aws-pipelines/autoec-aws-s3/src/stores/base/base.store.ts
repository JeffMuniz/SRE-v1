
import {
 action, computed, makeObservable, observable, reaction,
} from 'mobx';
import {
 clearPersistedStore, isHydrated, isPersisting, makePersistable, pausePersisting, startPersisting,
} from 'mobx-persist-store';
import {deepClone} from '~/utils';

const {NODE_ENV} = process.env;

export abstract class BaseStore<State> {

  protected initialState: State;

  @observable
  public state: State;

  get persistenceStatus() {
    return isPersisting(this);
  }

  constructor(state: State, options: BaseStore.Options = {
    persistence: null,
  }) {
    this.initialState = deepClone(state);
    this.state = state;

    makeObservable(this);
    if(options.persistence) {
      const {key, storage, expiresIn} = options.persistence;
      makePersistable(this, {
        name: key,
        storage,
        properties: [ 'state' ],
        expireIn: expiresIn,
      });
    }

    if(NODE_ENV !== 'production') {
      reaction(() => this.state, (value, prev) => {
        this.log(value, prev);
      });
    }
  }

  @action
  public resetState(): void {
    this.setStateObject(deepClone(this.initialState));
  }

  @action
  public setStateObject(state: State): void {
    Object.keys(this.state).forEach(key => {
      const value = state[key as keyof State];
      if(value === undefined) return;
      this.state[key as keyof State] = value;
    });
  }

  @action
  public setState(callback: (state: State) => void) {
    callback(this.state);
  }

  @computed
  get isHydrated(): boolean {
    return isHydrated(this);
  }

  public startPersistence(): void {
    startPersisting(this);
  }

  public pausePersistence(): void {
    pausePersisting(this);
  }

  public async clearPersistedData(): Promise<void> {
    await clearPersistedStore(this);
  }

  private log(value: State, prev: State): void {
    console.log(
      'Previous state:\n',
      JSON.parse(JSON.stringify(prev)),
      '\nCurrent state:',
      JSON.parse(JSON.stringify(value))
    );
  }

}
