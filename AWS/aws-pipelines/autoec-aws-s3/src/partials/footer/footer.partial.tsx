
import {FC, useMemo} from 'react';
import {observer} from 'mobx-react-lite';
import {ROUTES} from '~/consts';
import {AppDataStore} from '~/stores';
import {FBreadcrumb, Wrapper} from './footer.partial.styles';

const Footer: FC = () => {

  const {pathname} = AppDataStore.currentLocation;

  const currentStep = useMemo<number>(() => {
    switch (pathname) {
      case ROUTES.NAME_CONFIRMATION:
      case ROUTES.CONTACT_INFO:
      case ROUTES.CPF_CONFIRMATION:
      case ROUTES.CONTACT_PREFERENCE:
      case ROUTES.CONFIRMATION_CODE:
      case ROUTES.IDENTITY_CONFIRMATION: return 1;
      case ROUTES.ESTABLISHMENT_DATA: return 2;
      case ROUTES.FEES_AND_TARIFFS:
      case ROUTES.BANK_ACCOUNT: return 5;
      case ROUTES.DATA_CONFIRMATION: return 6;
    }

    if(pathname.includes(ROUTES.PAT_QUESTIONS)) {
      return 3;
    } else if(pathname.includes(ROUTES.ACQUIRERS)) {
      return 4;
    } else {
      return 1;
    }
  }, [ pathname ]);

  const hide = useMemo<boolean>(() => {
    return pathname === ROUTES.CONGRATS;
  }, [ pathname ]);

  const steps = useMemo<Array<Breadcrumb.Step>>(() => {
    const list = [{
      label: 'Dados do proprietário',
      isActive: false,
    }, {
      label: 'Dados do estabelecimento',
      isActive: false,
    }, {
      label: 'Produtos mac',
      isActive: false,
    }, {
      label: 'Maquininhas',
      isActive: false,
    }, {
      label: 'Condições comerciais',
      isActive: false,
    }, {
      label: 'Confirme seus dados',
      isActive: false,
    }];
    list[currentStep - 1].isActive = true;
    return list;
  }, [ currentStep ]);

  return (
    <Wrapper hide={hide}>
      <FBreadcrumb steps={steps} />
    </Wrapper>
  );
};

export default observer(Footer);
