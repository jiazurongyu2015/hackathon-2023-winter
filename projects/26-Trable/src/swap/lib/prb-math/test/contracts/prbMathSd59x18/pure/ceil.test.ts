import type { BigNumber } from "@ethersproject/bignumber";
import { Zero } from "@ethersproject/constants";
import { expect } from "chai";
import { toBn } from "evm-bn";
import forEach from "mocha-each";

import { MAX_SD59x18, MAX_WHOLE_SD59x18, MIN_SD59x18, MIN_WHOLE_SD59x18, PI } from "../../../../src/constants";
import { PRBMathSD59x18Errors } from "../../../../src/errors";
import { ceil } from "../../../../src/functions";

export function shouldBehaveLikeCeil(): void {
  context("when x is zero", function () {
    it("returns 0", async function () {
      const x: BigNumber = Zero;
      const expected: BigNumber = Zero;
      expect(expected).to.equal(await this.contracts.prbMathSd59x18.doCeil(x));
      expect(expected).to.equal(await this.contracts.prbMathSd59x18Typed.doCeil(x));
    });
  });

  context("when x is not zero", function () {
    context("when x is negative", function () {
      const testSets = [
        MIN_SD59x18,
        MIN_WHOLE_SD59x18,
        toBn("-1e18"),
        toBn("-4.2"),
        PI.mul(-1),
        toBn("-2"),
        toBn("-1.125"),
        toBn("-1"),
        toBn("-0.5"),
        toBn("-0.1"),
      ];

      forEach(testSets).it("takes %e and returns the correct value", async function (x: BigNumber) {
        const expected: BigNumber = ceil(x);
        expect(expected).to.equal(await this.contracts.prbMathSd59x18.doCeil(x));
        expect(expected).to.equal(await this.contracts.prbMathSd59x18Typed.doCeil(x));
      });
    });

    context("when x is positive", function () {
      context("when x > max whole sd59x18", function () {
        const testSets = [MAX_WHOLE_SD59x18.add(1), MAX_SD59x18];

        forEach(testSets).it("takes %e and reverts", async function (x: BigNumber) {
          await expect(this.contracts.prbMathSd59x18.doCeil(x)).to.be.revertedWith(PRBMathSD59x18Errors.CEIL_OVERFLOW);
          await expect(this.contracts.prbMathSd59x18Typed.doCeil(x)).to.be.revertedWith(
            PRBMathSD59x18Errors.CEIL_OVERFLOW,
          );
        });
      });

      context("when x <= max whole sd59x18", function () {
        const testSets = [
          toBn("0.1"),
          toBn("0.5"),
          toBn("1"),
          toBn("1.125"),
          toBn("2"),
          PI,
          toBn("4.2"),
          toBn("1e18"),
          MAX_WHOLE_SD59x18,
        ];

        forEach(testSets).it("takes %e and returns the correct value", async function (x: BigNumber) {
          const expected: BigNumber = ceil(x);
          expect(expected).to.equal(await this.contracts.prbMathSd59x18.doCeil(x));
          expect(expected).to.equal(await this.contracts.prbMathSd59x18Typed.doCeil(x));
        });
      });
    });
  });
}
