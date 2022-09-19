import {FC, useCallback} from 'react';
import {useHistory} from 'react-router-dom';
import {ROUTES} from '~/consts';
import {
 ContinueButton, FATPageSubtitle, FATSpacer, Wrapper,
} from './fees-and-tariffs.page.styles';
import Table from './table/table';

const FeesAndTariffsPage: FC = () => {

  const history = useHistory();

  const handleContinueButtonClick = useCallback(() => {
    history.push(ROUTES.BANK_ACCOUNT);
  }, [ history ]);

  return (
    <Wrapper>
      <FATPageSubtitle>
        taxas e tarifas
      </FATPageSubtitle>
      <Table />
      <FATSpacer />
      <ContinueButton onClick={handleContinueButtonClick}>
        continuar
      </ContinueButton>
    </Wrapper>
  );
};

export default FeesAndTariffsPage;
