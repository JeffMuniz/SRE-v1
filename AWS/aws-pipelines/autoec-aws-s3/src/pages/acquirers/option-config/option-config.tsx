
import {FC, useState} from 'react';
import {observer} from 'mobx-react-lite';
import {AcquirersPageStore} from '../acquirers.page.store';
import Form from './form/form';
import {
  CieloLogo, FindAffiliationCode, GetnetLogo, OCCheckbox, OCPageSubtitle, RedeLogo, SafrapayLogo, Wrapper,
} from './option-config.styles';
import SlipModal from './slip-modal/slip-modal';

const OptionConfig: FC = () => {

  const [ state, setState ] = useState({
    showAffiliationCodeHelper: false,
    hasSlip: false,
  });

  const handleFindAffiliationCodeClick = () => {
    setState(prev => ({
      ...prev,
      showAffiliationCodeHelper: true,
    }));
  };

  const handleAffiliationCodeHelperCloseClick = () => {
    setState(prev => ({
      ...prev,
      showAffiliationCodeHelper: false,
    }));
  };

  const {acquirer} = AcquirersPageStore.state;

  return (
    <Wrapper>
      <OCPageSubtitle>
        insira o(s) número(s) do estabelecimento
      </OCPageSubtitle>
      <FindAffiliationCode onClick={handleFindAffiliationCodeClick}>
        onde encontrar o código de filiação
      </FindAffiliationCode>
      <OCCheckbox>
        {acquirer === 'cielo' && <CieloLogo />}
        {acquirer === 'getnet' && <GetnetLogo />}
        {acquirer === 'rede' && <RedeLogo />}
        {acquirer === 'safrapay' && <SafrapayLogo />}
      </OCCheckbox>
      <Form />
      <SlipModal
        acquirer={acquirer}
        onCloseClick={handleAffiliationCodeHelperCloseClick}
        show={state.showAffiliationCodeHelper}
      />
    </Wrapper>
  );
};

export default observer(OptionConfig);
