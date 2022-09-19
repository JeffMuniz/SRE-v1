
import {FC, useEffect} from 'react';
import {observer} from 'mobx-react-lite';
import {useRouteMatch} from 'react-router-dom';
import {ROUTES} from '~/consts';
import {AcquirersPageStore} from './acquirers.page.store';
import {Wrapper} from './acquirers.page.styles';
import OptionConfig from './option-config/option-config';
import Options from './options/options';

const route = `${ROUTES.ACQUIRERS}/:acquirer`;

const AcquirersPage: FC = () => {

  const match = useRouteMatch<AcquirersPage.RouteMatchParams>(route);

  useEffect(() => {
    AcquirersPageStore.setState(state => {
      state.acquirer = match?.params.acquirer;
    });
  }, [ match?.params.acquirer ]);

  const {acquirer} = AcquirersPageStore.state;

  return (
    <Wrapper>
      {!acquirer ? (
        <Options />
      ) : (
        <OptionConfig />
      )}
    </Wrapper>
  );
};

export default observer(AcquirersPage);
