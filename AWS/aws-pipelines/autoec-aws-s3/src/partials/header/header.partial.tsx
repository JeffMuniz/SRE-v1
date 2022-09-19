import {FC, useMemo} from 'react';
import {observer} from 'mobx-react-lite';
import {ROUTES} from '~/consts';
import {AppDataStore} from '~/stores';
import {
 HBackButton, HPageSubtitle, HProgressBar, InnerWrapper, Wrapper,
} from './header.partial.styles';

const Header: FC = () => {

  const {pathname} = AppDataStore.currentLocation;

  const currentStep = useMemo(() => {
    switch (pathname) {
      case ROUTES.HOME: return 0;
      case ROUTES.NAME_CONFIRMATION: return 1;
      case ROUTES.CPF_CONFIRMATION: return 2;
      case ROUTES.CONTACT_INFO: return 3;
      case ROUTES.CONTACT_PREFERENCE: return 4;
      case ROUTES.CONFIRMATION_CODE: return 5;
      case ROUTES.IDENTITY_CONFIRMATION: return 6;
      case ROUTES.ESTABLISHMENT_DATA: return 7;
      case ROUTES.FEES_AND_TARIFFS: return 10;
      case ROUTES.BANK_ACCOUNT: return 11;
      case ROUTES.DATA_CONFIRMATION: return 12;
    }

    if(pathname.includes(ROUTES.PAT_QUESTIONS)) {
      return 8;
    } else if(pathname.includes(ROUTES.ACQUIRERS)) {
      return 9;
    } else {
      return 1;
    }
  }, [ pathname ]);

  const handleBackButtonClick = () => window.history.back();

  const hide = useMemo<{ mobile: boolean; tablet: boolean; }>(() => ({
    mobile: pathname === ROUTES.CONGRATS,
    tablet: pathname === ROUTES.HOME ||
    pathname === ROUTES.CONGRATS,
  }), [ pathname ]);

  const title = useMemo<string>(() => {
    switch (pathname) {
      case ROUTES.NAME_CONFIRMATION:
      case ROUTES.CONTACT_INFO:
      case ROUTES.CPF_CONFIRMATION:
      case ROUTES.CONTACT_PREFERENCE:
      case ROUTES.CONFIRMATION_CODE:
      case ROUTES.IDENTITY_CONFIRMATION: return 'dados do proprietário';
      case ROUTES.ESTABLISHMENT_DATA: return 'dados do estabelecimento';
      case ROUTES.FEES_AND_TARIFFS:
      case ROUTES.BANK_ACCOUNT: return 'condições comerciais';
      case ROUTES.DATA_CONFIRMATION: return 'resumo';
    }
    if(pathname.includes(ROUTES.PAT_QUESTIONS)) {
      return 'produtos';
    } else if(pathname.includes(ROUTES.ACQUIRERS)) {
      return 'maquininhas';
    } else {
      return '';
    }
  }, [ pathname ]);

  return (
    <Wrapper hide={hide.mobile} hideTablet={hide.tablet}>
      <InnerWrapper>
        <HBackButton onClick={handleBackButtonClick} />
        <HPageSubtitle>
          {title}
        </HPageSubtitle>
        {currentStep > 0 && (
          <HProgressBar
            currentStep={currentStep}
            steps={12}
          />
        )}
      </InnerWrapper>
    </Wrapper>
  );
};

export default observer(Header);
