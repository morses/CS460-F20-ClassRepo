// Node.js version
//const sum = require('./sum');
// ES 6 version
import {sum} from '../../GetStarted/wwwroot/js/sum'

test('sum(1,2) to equal 3', () => {
  expect(sum(1, 2)).toBe(3);
  //expect(3).toBe(3);
});