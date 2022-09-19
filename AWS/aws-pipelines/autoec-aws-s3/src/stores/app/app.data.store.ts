
import {Action, Location} from 'history';
import {action, computed} from 'mobx';

import {BaseStore} from '~/stores/base/base.store';

class Store extends BaseStore<AppDataStore.State> {

  private static readonly HISTORY_LIMIT = 100;

  constructor() {
    super({
      browsing: {
        history: [ ],
      },
    });
  }

  @computed
  public get currentLocation(): ExternalModules.History.Location {
    const {history} = this.state.browsing;
    return history[history.length - 1]?.location;
  }

  @computed
  public get lastLocation(): ExternalModules.History.Location {
    const {history} = this.state.browsing;
    if(history.length < 2) {
      return null;
    }
    return history[history.length - 2]?.location;
  }

  @action
  public handleNavigationUpdate(event: {
    action: Action,
    location: Location,
  }): void {
    const {length} = this.state.browsing.history;
    if(length >= Store.HISTORY_LIMIT) {
      const start = (length + 1) - Store.HISTORY_LIMIT;
      this.state.browsing.history = this.state.browsing.history
        .slice(start, Store.HISTORY_LIMIT)
        .concat(event);
    } else {
      this.state.browsing.history.push(event);
    }
  }

}

export const AppDataStore = new Store();
