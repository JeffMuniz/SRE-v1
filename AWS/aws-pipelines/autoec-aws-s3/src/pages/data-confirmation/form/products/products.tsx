
import {FC, useCallback} from 'react';
import {useHistory} from 'react-router';
import {ROUTES} from '~/consts';
import Information from './information/information';
import {
  EditLabel, TabletInlineWrapper, Wrapper,
} from './products.styles';

const Products: FC = () => {
  const history = useHistory();

  const navigateToProductsPage = useCallback(() => {
    history.push(`${ROUTES.PAT_QUESTIONS}/vr-acceptance`);
  }, [ history ]);

  return (
    <Wrapper>
      <EditLabel onClick={navigateToProductsPage}>
        editar
      </EditLabel>
      <TabletInlineWrapper>
        <Information type="vr" />
        <Information type="va" />
      </TabletInlineWrapper>
    </Wrapper>
  );
};

export default Products;
