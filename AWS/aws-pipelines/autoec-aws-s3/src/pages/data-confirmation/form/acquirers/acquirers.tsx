
import {FC, useCallback} from 'react';
import {observer} from 'mobx-react-lite';
import {useHistory} from 'react-router';
import {ROUTES} from '~/consts';
import {CartDataStore} from '~/stores';
import {
 ACheckbox, AcquirerWrapper, CieloLogo, DetailLabel, DetailWrapper, EditLabel, GetnetLogo, PagSeguroLogo, RedeLogo, SafrapayLogo, StoneLogo, TabletInlineWrapper, Wrapper,
} from './acquirers.styles';

const Acquirers: FC = () => {
  const {acquirers} = CartDataStore.state;
  const history = useHistory();

  const navigateToAcquirersPage = useCallback(() => {
    history.push(ROUTES.ACQUIRERS);
  }, [ history ]);

  return (
    <Wrapper>
      <EditLabel onClick={navigateToAcquirersPage}>
        editar
      </EditLabel>
      <TabletInlineWrapper>
        {acquirers.map(a => (
          <AcquirerWrapper key={a.name}>
            <ACheckbox checked onChange={() => { }}>
              {a.name === 'macna' && <CieloLogo />}
              {a.name === 'getnet' && <GetnetLogo />}
              {a.name === 'pag-seguro' && <PagSeguroLogo />}
              {a.name === 'rede' && <RedeLogo />}
              {a.name === 'safrapay' && <SafrapayLogo />}
              {a.name === 'stone' && <StoneLogo />}
            </ACheckbox>
            {a.affiliationCodes.length > 0 && (
              <DetailWrapper>
                <DetailLabel>
                  número(s) do estab.:
                </DetailLabel>
                {a.affiliationCodes.map(ac => (
                  <DetailLabel key={`${a.name}-${ac}`}>
                    {ac}
                  </DetailLabel>
                ))}
              </DetailWrapper>
            )}
          </AcquirerWrapper>
        ))}
      </TabletInlineWrapper>
    </Wrapper>
  );
};

export default observer(Acquirers);
