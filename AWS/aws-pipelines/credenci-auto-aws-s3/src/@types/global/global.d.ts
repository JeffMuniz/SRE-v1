
import {
 ChangeEvent, ChangeEventHandler, FC, FocusEventHandler, FormEventHandler, MouseEvent, MouseEventHandler, MutableRefObject, ReactNode, Ref,
} from 'react';
import {
 AxiosInstance, AxiosRequestConfig, AxiosResponse,
} from 'axios';
import {Action, Location} from 'history';
import {StorageController} from 'mobx-persist-store';
import {StyledComponent} from 'styled-components';

declare global {
  namespace ExternalModules {
    namespace Axios {
      export type {
        AxiosInstance,
        AxiosRequestConfig,
        AxiosResponse,
      };
    }
    namespace History {
      export type {
        Action,
        Location,
      };
    }
    namespace React {
      export type  {
        ChangeEvent,
        ChangeEventHandler,
        FC,
        FocusEventHandler,
        FormEventHandler,
        MouseEvent,
        MouseEventHandler,
        MutableRefObject,
        ReactNode,
        Ref,
      };
    }
    namespace MobxStorePersist {
      export type {StorageController};
    }
    namespace StyledComponents {
      export type {StyledComponent};
    }
  }
}
