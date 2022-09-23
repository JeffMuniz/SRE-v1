import {
 FC, memo, useMemo,
} from 'react';
import {isEqual} from '~/utils';
import {
 Content, Item, Row, Wrapper,
} from './table.component.styles';

const Table: FC<Table.Props> = ({
  className,
  highlightedColumns = [ ],
  matrix = [ [ ] ],
}) => {

  const isMatrixValid = useMemo(() => {
    const {length} = matrix[0];
    const hasDissident = matrix.find(m => m.length !== length);
    return !hasDissident;
  }, [ matrix ]);

  if(!isMatrixValid) throw new Error('Invalid matrix.');

  return (
    <Wrapper className={className}>
      {matrix.map((x, indexX) => (
        <Row key={`${x}_${indexX}`}>
          {x.map((y, indexY) => (
            <Item
              key={`${y}_${indexY}`}
              highlight={highlightedColumns.includes(indexY)}
            >
              <Content>{y}</Content>
            </Item>
          ))}
        </Row>
      ))}
    </Wrapper>
  );
};

export default memo(Table, isEqual);
